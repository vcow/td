using System;
using System.Collections.Generic;
using AI;
using Models;
using Models.Enemies;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using Views.Enemies;

namespace Views.Field
{
    /// <summary>
    /// Представление игрового поля.
    /// </summary>
    [DisallowMultipleComponent]
    public class FieldView : MonoBehaviour
    {
        private Collider _fieldCollider;
        private Renderer _renderer;

        private bool _markerIsVisible = true;

        private readonly Dictionary<CellModel, GameObject> _cellsMap = new Dictionary<CellModel, GameObject>();

        private Transform _transform;

        [Serializable]
        private class ClickFieldEvent : UnityEvent<Vector2>
        {
        }

        [SerializeField] private Transform _marker;
        [SerializeField] private ClickFieldEvent _clickFieldEvent = new ClickFieldEvent();

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _fieldCollider = GetComponent<Collider>();
            Assert.IsNotNull(_fieldCollider);

            _renderer = GetComponent<Renderer>();
        }

        private void OnDestroy()
        {
            _clickFieldEvent.RemoveAllListeners();
        }

        private void OnMouseDown()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!_fieldCollider.Raycast(ray, out hit, Camera.main.farClipPlane)) return;

            var texCoord = hit.textureCoord;
            _clickFieldEvent.Invoke(new Vector2(Mathf.Clamp01(1f - texCoord.x), Mathf.Clamp01(1f - texCoord.y)));
        }

        /// <summary>
        /// Физические размеры поля.
        /// </summary>
        public Vector2 FieldSize
        {
            get
            {
                if (_renderer == null) return Vector2.zero;
                
                var s = _renderer.bounds;
                return new Vector2(s.size.x, s.size.z);
            }
        }

        /// <summary>
        /// Настройка маркера.
        /// </summary>
        /// <param name="size">Размер в коэффициентах по отношению к размеру поля.</param>
        /// <param name="color">Цвет маркера.</param>
        public void SetupMarker(Vector2 size, Color? color = null)
        {
            if (_marker == null || !MarkerIsVisible) return;
            
            _marker.transform.localScale = new Vector3(size.x, 1, size.y);

            if (color == null) return;
            var r = _marker.GetComponent<Renderer>();
            var mat = r != null ? r.material : null;
            if (mat != null) mat.color = color.Value;
        }

        /// <summary>
        /// Видимость маркера.
        /// </summary>
        public bool MarkerIsVisible
        {
            get { return _markerIsVisible; }
            set
            {
                if (value == _markerIsVisible) return;
                _markerIsVisible = value;
                if (_marker != null)
                {
                    _marker.gameObject.SetActive(_markerIsVisible);
                }
            }
        }

        /// <summary>
        /// Позиция маркера, задается как коэффициент по отношению к размерам поля.
        /// </summary>
        public Vector2 MarkerPosition
        {
            set
            {
                if (_marker == null) return;
                _marker.transform.localPosition = Pos2World(value) + new Vector3(0, 0.005f, 0);
            }
        }

        /// <summary>
        /// Преобразовать позицию, заданную как коэффициент в мировые координаты.
        /// </summary>
        /// <param name="pos">Нормализованная позиция.</param>
        /// <returns>Мировые координаты.</returns>
        public Vector3 Pos2World(Vector2 pos)
        {
            var sz = FieldSize;
            return new Vector3(pos.x * sz.x, _transform.position.y, pos.y * sz.y);
        }

        /// <summary>
        /// Создать представление врага.
        /// </summary>
        /// <param name="enemy">Логика создаваемого врага.</param>
        public void InstantiateEnemy(EnemyLogic enemy)
        {
            GameObject prefab = null;
            switch (enemy.Model.Type)
            {
                case EnemyType.Small:
                    prefab = GameModel.Instance.GameSettings.SmallEnemyPrefab;
                    break;
                case EnemyType.Medium:
                    prefab = GameModel.Instance.GameSettings.MediumEnemyPrefab;
                    break;
                case EnemyType.Large:
                    prefab = GameModel.Instance.GameSettings.LargeEnemyPrefab;
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (prefab == null)
            {
                Debug.LogWarning("Prefab for this kind of enemy is not specified.");
                return;
            }

            var instance = Instantiate(prefab, transform);
            var enemyView = instance.GetComponent<EnemyView>();
            if (enemyView != null)
            {
                enemyView.Logic = enemy;
            }
            else
            {
                Debug.LogWarning("Enemy view must have behaviour of EnemyView class.");
            }
        }

        /// <summary>
        /// Создать представление для элемента сцены.
        /// </summary>
        /// <param name="cell">Ячейка, для которой создается представление.</param>
        /// <param name="position">Позиция представления заданная как коэффициент по отношению к размерам поля.</param>
        public void InstantiateItem(CellModel cell, Vector2 position)
        {
            if (_cellsMap.ContainsKey(cell))
            {
                Debug.LogWarning("Cell {0} already have instance.");
                return;
            }
            
            GameObject prefab = null;
            switch (cell.ItemType)
            {
                case ItemType.TinnyTower:
                    prefab = GameModel.Instance.GameSettings.TinnyTowerPrefab;
                    break;
                case ItemType.SmallTower:
                    prefab = GameModel.Instance.GameSettings.SmallTowerPrefab;
                    break;
                case ItemType.MediumTower:
                    prefab = GameModel.Instance.GameSettings.MediumTowerPrefab;
                    break;
                case ItemType.LargeTower:
                    prefab = GameModel.Instance.GameSettings.LargeTowerPrefab;
                    break;
                case ItemType.HugeTower:
                    prefab = GameModel.Instance.GameSettings.HugeTowerPrefab;
                    break;
                case ItemType.Rock:
                    prefab = GameModel.Instance.GameSettings.RockPrefab;
                    break;
                case ItemType.Emitter:
                    prefab = GameModel.Instance.GameSettings.EmitterPrefab;
                    break;
                case ItemType.Target:
                    prefab = GameModel.Instance.GameSettings.TargetPrefab;
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (prefab == null)
            {
                Debug.LogWarning("Prefab for this kind of item is not specified.");
                return;
            }

            var instance = Instantiate(prefab, transform);
            instance.transform.localPosition = Pos2World(position);
            _cellsMap[cell] = instance;
        }

        /// <summary>
        /// Удалить представление для указанной ячейки.
        /// </summary>
        /// <param name="cell">Ячейка, для которой удаляется представление.</param>
        /// <returns>Возвращает <code>true</code>, если удаление прошло успешно.</returns>
        public bool ClearCell(CellModel cell)
        {
            GameObject instance;
            if (_cellsMap.TryGetValue(cell, out instance))
            {
                _cellsMap.Remove(cell);
                Destroy(instance);
                return true;
            }

            return false;
        }
    }
}