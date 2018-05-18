using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Views.Field
{
    public class FieldView : MonoBehaviour
    {
        private Collider _fieldCollider;
        
        [Serializable]
        private class ClickFieldEvent : UnityEvent<Vector2>
        {
        }
        
        [SerializeField]
        private ClickFieldEvent _clickFieldEvent;

        private void Start()
        {
            _fieldCollider = GetComponent<Collider>();
            Assert.IsNotNull(_fieldCollider);
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

            _clickFieldEvent.Invoke(hit.textureCoord);
        }
    }
}