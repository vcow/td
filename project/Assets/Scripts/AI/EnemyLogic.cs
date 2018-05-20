using System.Collections.Generic;
using System.Linq;
using Models.Enemies;
using UnityEngine;

namespace AI
{
    public class EnemyLogic : ILogic
    {
        private readonly IEnemy _enemy;
        private readonly IEnumerable<Vector3> _path;
        private readonly IGameLogic _gameLogic;

        private int _health;
        private Vector3 _calcPosition;
        private float _totalTime;
        private bool _targetReached;

        public EnemyLogic(IEnemy enemy, IEnumerable<Vector3> path, IGameLogic gameLogic)
        {
            _enemy = enemy;
            _path = path;
            _gameLogic = gameLogic;

            _health = enemy.Health;
            _calcPosition = _path.FirstOrDefault();
            _totalTime = 0;
        }

        public IEnemy Model
        {
            get { return _enemy; }
        }

        public void Update(float deltaTime)
        {
            _totalTime += deltaTime;

            if (_health <= 0 || _targetReached) return;
            
            bool targetReached;
            _calcPosition = DoCalcPosition(_totalTime, out targetReached);

            if (targetReached)
            {
                // Win
                _gameLogic.HitTarget(_enemy.Damage);
                _targetReached = true;
            }
        }

        /// <summary>
        /// Попадание по врагу.
        /// </summary>
        /// <param name="hitPoints">Нанесенный ущерб.</param>
        public void Hit(int hitPoints)
        {
            var isLive = _health > 0;
            _health -= hitPoints;
            if (isLive && _health <= 0)
            {
                // Fail
                _gameLogic.EnemyDie(this);
            }
        }

        /// <summary>
        /// Текущее значение жизни.
        /// </summary>
        public int Health
        {
            get { return _health; }
        }

        /// <summary>
        /// Получить рассчетное положение врага.
        /// </summary>
        /// <param name="extraTime">Смещение по времени, позволяет рассчитывать позицию врага
        /// в которой он окажется через указанное время.</param>
        /// <returns>Рассчетная позиция.</returns>
        public Vector3 GetPosition(float extraTime = 0)
        {
            bool targetReached;
            return Mathf.Approximately(extraTime, 0)
                ? _calcPosition
                : DoCalcPosition(_totalTime + extraTime, out targetReached);
        }

        private Vector3 DoCalcPosition(float time, out bool targetReached)
        {
            var speed = _enemy.Speed;
            var index = Mathf.FloorToInt(time / speed);
            if (index >= _path.Count() - 1)
            {
                targetReached = true;
                return _path.LastOrDefault();
            }

            targetReached = false;
            if (index <= 0 && Mathf.Approximately(time, 0))
            {
                return _path.FirstOrDefault();
            }

            var stepFrom = _path.ElementAt(index);
            var stepTo = _path.ElementAt(index + 1);
            var k = Mathf.Clamp01((time - speed * index) / speed);
            return Vector3.Lerp(stepFrom, stepTo, k);
        }
    }
}