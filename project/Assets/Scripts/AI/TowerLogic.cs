using System.Collections.Generic;
using System.Linq;
using Models.Towers;
using UnityEngine;
using UnityEngine.Assertions;

namespace AI
{
    public class TowerLogic : LogicBase
    {
        private readonly ITower _tower;
        private readonly List<EnemyLogic> _enemies;
        private readonly float _cellSize;
        private readonly IGameLogic _gameLogic;
        private readonly Vector3 _position;

        private bool _isRecharged;

        private readonly float _sqrRadius;
        private readonly List<EnemyLogic> _accessibleEnemies = new List<EnemyLogic>();

        private readonly List<ShotLogic> _shots = new List<ShotLogic>();

        public TowerLogic(ITower tower, Vector3 position, float cellSize,
            List<EnemyLogic> enemies, IGameLogic gameLogic)
        {
            _tower = tower;
            _position = position;
            _cellSize = cellSize;
            _enemies = enemies;
            _gameLogic = gameLogic;

            Assert.IsNotNull(_tower.Weapon);
            _sqrRadius = _tower.Weapon.Radius * cellSize;
            _sqrRadius *= _sqrRadius;
        }

        public ITower Model
        {
            get { return _tower; }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            for (var i = _shots.Count - 1; i >= 0; --i)
            {
                var shot = _shots[i];
                shot.Update(deltaTime);
                if (shot.IsHit)
                {
                    _shots.RemoveAt(i);
                }
            }

            if (_isRecharged) return;

            GetAccessibleEnemies();
            if (!_accessibleEnemies.Any()) return;

            Attack(_accessibleEnemies[Random.Range(0, _accessibleEnemies.Count - 1)]);
        }

        private void GetAccessibleEnemies()
        {
            _accessibleEnemies.Clear();
            _enemies.ForEach(enemy =>
            {
                if ((_position - enemy.GetPosition()).sqrMagnitude <= _sqrRadius)
                {
                    _accessibleEnemies.Add(enemy);
                }
            });
        }

        private void Attack(EnemyLogic enemy)
        {
            var shot = new ShotLogic(_tower.Weapon, enemy, _cellSize, _position);
            _shots.Add(shot);
            _gameLogic.Shoot(shot);
            Cooldown();
        }

        private void Cooldown()
        {
            _isRecharged = true;
            DelayCall(_tower.Weapon.RechargeTime, () => _isRecharged = false);
        }
    }
}