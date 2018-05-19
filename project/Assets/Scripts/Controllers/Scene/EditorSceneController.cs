using Models;
using UnityEngine;
using UnityEngine.Assertions;
using Views.Field;

namespace Controllers.Scene
{
    public class EditorSceneController : SceneControllerBase
    {
        private IItem _selectedItem;

        [SerializeField] private FieldView _field;

        protected override void Start()
        {
            base.Start();
            if (!IsInitialized) return;

            InitScene();
        }

        protected override void InitScene()
        {
            Assert.IsNotNull(_field);
            _field.SetupMarker(CalcMarkerSize());
            _field.MarkerPosition = CalcItemPosition(Vector2.zero);
        }

        public void OnClickField(Vector2 value)
        {
            _field.MarkerPosition = CalcItemPosition(value);
            if (_selectedItem == null) return;
        }

        private static Vector2 CalcMarkerSize()
        {
            var fm = GameModel.Instance.FieldModel;
            return fm != null ? new Vector2(1f / fm.Size.x, 1f / fm.Size.y) : Vector2.one;
        }

        private static Vector2 CalcItemPosition(Vector2 rawPosition)
        {
            var fm = GameModel.Instance.FieldModel;
            if (fm == null) return Vector2.zero;

            var markerSize = CalcMarkerSize();
           return new Vector2(Step(markerSize.x, rawPosition.x), Step(markerSize.y, rawPosition.y))
                - (Vector2.one - markerSize) * 0.5f;
        }

        private static float Step(float step, float length)
        {
            return Mathf.Clamp(Mathf.Floor(length / step) * step, 0.0f, length);
        }
    }
}