#define EDITOR_MODE

using System;
using System.IO;
using System.Xml.Serialization;
using Models;
using Models.Enemies;
using Models.Towers;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Controllers.Scene
{
    public class EditorSceneController : FieldSceneControllerBase
    {
        [SerializeField] private Transform _menuContainer;
        [SerializeField] private GameObject _menuButtonPrefab;
        [SerializeField] private Text _moneyText;

        private IItem _selectedItem;

#if !EDITOR_MODE
        private FieldModel _sourceField;
#endif

        protected override void InitScene()
        {
            base.InitScene();

            CreateMenu();

            Field.SetupMarker(CalcMarkerSize());
            Field.MarkerPosition = CalcItemPosition(Vector2.zero);

#if !EDITOR_MODE
            _sourceField = GameModel.Instance.FieldModel.Clone();
#endif

            if (_moneyText != null)
            {
                var gm = GameModel.Instance;
                gm.MoneyChangedEvent += OnMoneyChanged;
                OnMoneyChanged(gm.Money);
            }
        }

        private void OnMoneyChanged(decimal value)
        {
            _moneyText.text = string.Format("money: {0}", value);
        }

        private void OnDestroy()
        {
            if (!IsInitialized) return;
            
            foreach (var button in _menuContainer.GetComponentsInChildren<Button>())
            {
                button.onClick.RemoveAllListeners();
            }

            if (_moneyText != null)
            {
                GameModel.Instance.MoneyChangedEvent -= OnMoneyChanged;
            }
        }

        public override void OnClickField(Vector2 value)
        {
            base.OnClickField(value);

            var fm = GameModel.Instance.FieldModel;
            var coord = Pos2Coord(value);
            var cell = fm.GetCellByCoord(coord);
            
            if (_selectedItem == null)
            {
                // Delete

                if (cell == null) return;
                
                Field.ClearCell(cell);

                var t = cell.Item as ITower;
                if (t != null)
                {
                    // Сравнить удаляемую ячейку с прежним содержимым. Если содержимое не менялось,
                    // происходит удаление в рамках сессии редактирования, юзеру возвращается полная сумма
                    // за башню. Если содержимое другое, удаляется башня из предыдущей сессии,
                    // юзеру возвращается цена продажи.
#if !EDITOR_MODE
                    var sourceCell = _sourceField.GetCellByCoord(coord);
                    if (sourceCell == cell && sourceCell.ItemType == cell.ItemType)
                    {
                        GameModel.Instance.Money += t.BuyPrice;
                    }
                    else
                    {
                        GameModel.Instance.Money += t.SellPrice;
                    }
#endif
                        
                    // Башни стоят на камнях, поэтому если удаляется башня, заменить ее на камень.
                    cell.ItemType = ItemType.Rock;
                    Field.InstantiateItem(cell, Coord2Pos(cell.Coordinate));
                }
#if EDITOR_MODE
                else
                {
                    fm.ClearCell(cell);
                }
#endif

                return;
            }

            var tower = _selectedItem as ITower;
            if (tower != null)
            {
#if !EDITOR_MODE
                if (GameModel.Instance.Money < tower.BuyPrice)
                {
                    Debug.LogWarning("Not enough money!");
                    return;
                }
#endif
                if (cell == null || cell.ItemType != ItemType.Rock)
                {
                    Debug.LogWarning("Tower can be placed on the rocks only.");
                    return;
                }
                
                Field.ClearCell(cell);
                cell.ItemType = tower.Type;
                Field.InstantiateItem(cell, Coord2Pos(cell.Coordinate));
#if !EDITOR_MODE
                GameModel.Instance.Money -= tower.BuyPrice;
#endif
            }
#if EDITOR_MODE
            else
            {
                if (cell != null)
                {
                    Field.ClearCell(cell);
                    fm.ClearCell(cell);
                }

                cell = new CellModel
                {
                    ItemType = _selectedItem.Type,
                    Coordinate = coord
                };
                
                fm.AddCell(cell);
                Field.InstantiateItem(cell, Coord2Pos(cell.Coordinate));
            }
#endif
        }

        private void CreateMenu()
        {
            Assert.IsNotNull(_menuContainer);
            Assert.IsNotNull(_menuButtonPrefab);

            var items = new IItem[]
            {
                null, FieldItemsLibrary.TinyTower, FieldItemsLibrary.SmallTower, FieldItemsLibrary.MediumTower,
                FieldItemsLibrary.LargeTower, FieldItemsLibrary.HugeTower
#if EDITOR_MODE
                , FieldItemsLibrary.Rock, FieldItemsLibrary.Emitter, FieldItemsLibrary.Target
#endif
            };

            foreach (var item in items)
            {
                var bn = Instantiate(_menuButtonPrefab, _menuContainer);
                var button = bn.GetComponent<Button>();
                Assert.IsNotNull(button);
                ApplyButton(button, item);
            }
            
#if EDITOR_MODE
            var b = Instantiate(_menuButtonPrefab, _menuContainer);
            var savedButton = b.GetComponent<Button>();
            Assert.IsNotNull(savedButton);
            var label = savedButton.GetComponentInChildren<Text>();
            if (label != null) label.text = "Save";
            savedButton.onClick.AddListener(OnSave);
#endif
        }
        
#if EDITOR_MODE
        private void OnSave()
        {
            var serializer = new XmlSerializer(typeof(FieldModel), new Type[]
            {
                typeof(CellModel),
                typeof(WaveModel),
                typeof(ItemType),
                typeof(EnemyType),
                typeof(WaveEnemyEntry)
            });

            var fm = GameModel.Instance.FieldModel;
            var savePath = Path.Combine(Application.persistentDataPath, string.Format("{0}.xml", fm.Name));
            if (File.Exists(savePath))
            {
                Debug.LogWarning(string.Format("File {0} already exists.", savePath));
                return;
            }
            
            var fs = new FileStream(savePath, FileMode.Create);
            serializer.Serialize(fs, fm);
            fs.Close();
            
            Debug.Log(string.Format("Field was saved to: {0}", savePath));
        }
#endif

        private void ApplyButton(Button button, IItem item)
        {
            var label = button.GetComponentInChildren<Text>();
            if (label != null)
            {
                if (item != null)
                {
                    var tower = item as ITower;
                    label.text = item.Name + (tower != null ? string.Format(" ({0})", tower.BuyPrice) : "");
                }
                else
                {
                    label.text = "Delete";
                }
            }

            button.onClick.AddListener(() => _selectedItem = item);
        }
    }
}