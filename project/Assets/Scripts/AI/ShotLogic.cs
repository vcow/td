using Models.Towers;
using UnityEngine;

namespace AI
{
    public class ShotLogic : ILogic
    {
        private readonly IWeapon _weapon;
        private readonly EnemyLogic _target;
        private readonly Vector3 _position;
        private readonly Vector3 _targetPosition;

        private float _totalTime;
        private readonly float _calcTime;

        public ShotLogic(IWeapon weapon, EnemyLogic target, float cellSize, Vector3 position)
        {
            _weapon = weapon;
            _target = target;
            _position = position;
            BulletPosition = position;

            var to = target.GetPosition();

            _totalTime = 0;
            _calcTime = Vector3.Magnitude(to - _position) / cellSize / weapon.BulletSpeed;
            _targetPosition = _target.GetPosition(_calcTime);
        }

        /// <summary>
        /// Текущее положение пули в мировых координатах.
        /// </summary>
        public Vector3 BulletPosition { get; private set; }

        /// <summary>
        /// Попадание.
        /// </summary>
        public bool IsHit { get; private set; }

        public void Update(float deltaTime)
        {
            if (IsHit) return;

            _totalTime += deltaTime;
            if (_totalTime >= _calcTime)
            {
                IsHit = true;
                BulletPosition = _targetPosition;
                _target.Hit(_weapon.Damage);
                return;
            }

            BulletPosition = Vector3.Lerp(_position, _targetPosition, Mathf.Clamp01(_totalTime / _calcTime));
        }
    }
}