using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Enemies;
using UnityEngine;

namespace AI
{
    public class WaveLogic : LogicBase
    {
        private readonly WaveModel _wave;
        private readonly IGameLogic _gameLogic;
        
        private readonly List<EnemyType> _waitingEnemies  = new List<EnemyType>();
        
        public WaveLogic(WaveModel wave, IGameLogic gameLogic)
        {
            _wave = wave;
            _gameLogic = gameLogic;

            _wave.Enemies.ForEach(entry =>
            {
                for (var i = 0; i < entry.EnemiesCount; ++i)
                {
                    _waitingEnemies.Add(entry.Type);
                }
            });
            
            // Start
            Emit();
        }

        private void Emit()
        {
            if (_waitingEnemies.Any())
            {
                var randomIndex =Random.Range(0, _waitingEnemies.Count - 1);
                _gameLogic.SpawnEnemy(_waitingEnemies[randomIndex]);
                _waitingEnemies.RemoveAt(randomIndex);
                
                DelayCall(1f / _wave.EmissionSpeed, Emit);
            }
            else
            {
                // Finish
                _gameLogic.WaveFinished(this);
            }
        }
    }
}