using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers.Scene;
using Models;
using Models.Enemies;
using UnityEngine;
using UnityEngine.Assertions;

namespace AI
{
    public struct GameResult
    {
        // TODO: Add round result here.
    }

    public class GameLogic : LogicBase, IGameLogic
    {
        private readonly IGameController _controller;

        private FieldModel _field;
        private IEnumerator<WaveModel> _waveIterator;

        private readonly List<WaveLogic> _workingWaves = new List<WaveLogic>();
        private readonly List<EnemyLogic> _spawnedEnemies = new List<EnemyLogic>();

        private IEnumerable<Vector2Int> _path;

        public GameLogic(IGameController controller)
        {
            _controller = controller;
        }

        public void Start()
        {
            _field = GameModel.Instance.FieldModel;
            Assert.IsNotNull(_field);

            var emitter = _field.Cells.FirstOrDefault(cell => cell.ItemType == ItemType.Emitter);
            var target = _field.Cells.FirstOrDefault(cell => cell.ItemType == ItemType.Target);

            if (emitter == null || target == null)
            {
                Debug.LogWarning("Current field has no any Emitters or Tragets.");
                _controller.Lose(GetResult());
                return;
            }
            
            var pf = new PathFinder();
            _path = pf.CalcPath(emitter, target);
            if (!_path.Any())
            {
                Debug.LogWarning("Current field has no path from Emitter or Traget.");
                _controller.Lose(GetResult());
                return;
            }

            foreach (var p in _path)
            {
                _controller.DebugMarkCell(p);
            }

            _waveIterator = _field.Waves.GetEnumerator();
            ApplyNextWave();
        }

        private void ApplyNextWave()
        {
            Assert.IsNotNull(_waveIterator);

            if (_waveIterator.MoveNext())
            {
                ClearAllDelayedCalls();

                var wave = _waveIterator.Current;
                if (wave != null && wave.Enemies.Any())
                {
                    DelayCall(GameModel.Instance.GameSettings.NextWaveDelayTime, () => StartWave(wave));
                }
                else
                {
                    ApplyNextWave();
                }
            }
            else
            {
                _waveIterator.Dispose();
                _waveIterator = null;
            }
        }

        /// <summary>
        /// Завершить волну.
        /// </summary>
        /// <param name="wave">Завершаемая волна.</param>
        public void WaveFinished(WaveLogic wave)
        {
            if (_workingWaves.Contains(wave))
            {
                _workingWaves.Remove(wave);
            }
            else
            {
                Debug.LogWarning("Wave already was destroyed.");
            }
            
            ApplyNextWave();
        }

        /// <summary>
        /// Породить врага.
        /// </summary>
        /// <param name="type">Тип порождаемого врага.</param>
        public void SpawnEnemy(EnemyType type)
        {
            Debug.LogFormat("-- Spawned {0}", type);
        }

        private IEnumerator StartWaveCoroutine(float delay, WaveModel wave)
        {
            yield return new WaitForSeconds(delay);
            StartWave(wave);
        }

        private void StartWave(WaveModel wave)
        {
            Debug.Log("--- Start enemy wave.");
            var waveLogic = new WaveLogic(wave, this);
            _workingWaves.Add(waveLogic);
        }

        private GameResult GetResult()
        {
            // TODO: Build result.

            return new GameResult();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _spawnedEnemies.ForEach(enemy => enemy.Update(deltaTime));
            _workingWaves.ForEach(wave => wave.Update(deltaTime));
        }
    }
}