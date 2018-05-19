#define EDITOR_MODE

using Models;
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

        private IItem _selectedItem;

        private FieldModel _sourceField;

        protected override void InitScene()
        {
            base.InitScene();

            CreateMenu();

            Field.SetupMarker(CalcMarkerSize());
            Field.MarkerPosition = CalcItemPosition(Vector2.zero);

            _sourceField = GameModel.Instance.FieldModel.Clone();
        }

        private void OnDestroy()
        {
            foreach (var button in _menuContainer.GetComponentsInChildren<Button>())
            {
                button.onClick.RemoveAllListeners();
            }
        }

        public override void OnClickField(Vector2 value)
        {
            base.OnClickField(value);
            if (_selectedItem == null) return;

            // TODO: Check money
            
            var coord = Pos2Coord(value);
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
        }

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