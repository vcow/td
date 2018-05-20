using System.Collections.Generic;
using Models.Enemies;
using UnityEngine;

namespace AI
{
    public class EnemyLogic : ILogic
    {
        private readonly IEnemy _enemy;
        private readonly IEnumerable<Vector3> _path;
        private readonly IGameLogic _gameLogic;
        
        public EnemyLogic(IEnemy enemy, IEnumerable<Vector3> path, IGameLogic gameLogic)
        {
            _enemy = enemy;
            _path = path;
            _gameLogic = gameLogic;
        }
        
        public IEnemy Model
        {
            get { return _enemy; }
        }
        
        public void Update(float deltaTime)
        {
            // TODO:
        }
    }
}