using System;
using Models;
using UnityEngine;
using UnityEngine.Assertions;
using Views.Field;

namespace Controllers.Scene
{
    /// <summary>
    /// Базовый класс сцены, содержащей игровое поле.
    /// </summary>
    public abstract class FieldSceneControllerBase : SceneControllerBase
    {
        [SerializeField] private FieldView _field;
        
        protected FieldView Field
        {
            get { return _field; }
        }

        protected override void Start()
        {
            base.Start();
            if (!IsInitialized) return;

            InitScene();
        }

        protected override void InitScene()
        {
            Assert.IsNotNull(Field);
            PopulateField();
        }

        // ReSharper disable once MemberCanBeProtected.Global
        public virtual void OnClickField(Vector2 value)
        {
            Field.MarkerPosition = CalcItemPosition(value);
        }

        protected static Vector2 CalcMarkerSize()
        {
            var fm = GameModel.Instance.FieldModel;
            return new Vector2(1f / fm.Size.x, 1f / fm.Size.y);
        }

        protected static Vector2 CalcItemPosition(Vector2 rawPosition)
        {
            var markerSize = CalcMarkerSize();
            return new Vector2(Step(markerSize.x, rawPosition.x), Step(markerSize.y, rawPosition.y))
                   - (Vector2.one - markerSize) * 0.5f;
        }

        private static float Step(float step, float length)
        {
            return Mathf.Clamp(Mathf.Floor(length / step) * step, 0.0f, length);
        }

        private void PopulateField()
        {
            var fm = GameModel.Instance.FieldModel;
            foreach (var cell in fm.Cells)
            {
                Field.InstantiateItem(cell, Coord2Pos(cell.Coordinate));
            }
        }

        protected static Vector2 Coord2Pos(Vector2Int coord)
        {
            var markerSize = CalcMarkerSize();
            return new Vector2(coord.x * markerSize.x, coord.y * markerSize.y) + (markerSize - Vector2.one) * 0.5f;
        }

        protected static Vector2Int Pos2Coord(Vector2 pos)
        {
            var markerSize = CalcMarkerSize();
            return new Vector2Int(Mathf.FloorToInt(pos.x / markerSize.x), Mathf.FloorToInt(pos.y / markerSize.y));
        }
    }
}