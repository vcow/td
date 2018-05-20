using AI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Views.Towers
{
    [DisallowMultipleComponent]
    public class ShotView : MonoBehaviour
    {
        private Transform _transform;
        
        public ShotLogic Logic { set; private get; }

        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            Assert.IsNotNull(Logic);
            if (Logic.IsHit)
            {
                Destroy(gameObject);
            }
            else
            {
                _transform.localPosition = Logic.BulletPosition;
            }
        }
    }
}