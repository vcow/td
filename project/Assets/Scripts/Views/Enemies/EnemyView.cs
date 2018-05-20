using AI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Views.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        private Transform _transform;
        private bool _isDead;
        
        public EnemyLogic Logic { set; private get; }

        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            Assert.IsNotNull(Logic);
            if (_isDead) return;

            _isDead = Logic.Health <= 0;
            if (_isDead)
            {
                Die();
            }
            else
            {
                transform.localPosition = Logic.GetPosition();
            }
        }

        private void Die()
        {
            // TODO: Animate effect for the death.
            
            Destroy(gameObject);
        }
    }
}