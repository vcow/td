using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

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

        [Serializable]
        private class ClickFieldEvent : UnityEvent<Vector2>
        {
        }

        [SerializeField] private Transform _marker;
        [SerializeField] private ClickFieldEvent _clickFieldEvent;

        private void Start()
        {
            _fieldCollider = GetComponent<Collider>();
            Assert.IsNotNull(_fieldCollider);

            _renderer = GetComponent<Renderer>();
        }

        private void OnDestroy()
        {
            if (_clickFieldEvent != null)
            {
                _clickFieldEvent.RemoveAllListeners();
            }
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
                var sz = FieldSize;
                var t = _marker.transform;
                t.localPosition = new Vector3(value.x * sz.x, t.localPosition.y, value.y * sz.y);
            }
        }
    }
}