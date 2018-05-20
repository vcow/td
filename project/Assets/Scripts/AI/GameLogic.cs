using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers.Scene;
using Models;
using Models.Enemies;
using Models.Towers;
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

        private IEnumerator<WaveModel> _waveIterator;

        private readonly List<WaveLogic> _workingWaves = new List<WaveLogic>();
        private readonly List<EnemyLogic> _spawnedEnemies = new List<EnemyLogic>();
        private readonly List<TowerLogic> _towers = new List<TowerLogic>();

        private Vector3[] _path;
        private int _targetHealth;

        public GameLogic(IGameController controller)
        {
            _controller = controller;
        }

        public void Start()
        {
            var fm = GameModel.Instance.FieldModel;
            Assert.IsNotNull(fm);

            _targetHealth = fm.TargetHealth;
            var emitter = fm.Cells.FirstOrDefault(cell => cell.ItemType == ItemType.Emitter);
            var target = fm.Cells.FirstOrDefault(cell => cell.ItemType == ItemType.Target);

            if (emitter == null || target == null)
            {
                Debug.LogWarning("Current field has no any Emitters or Tragets.");
                _controller.Lose(GetResult());
                return;
            }

            var pf = new PathFinder();
            var coordPath = pf.CalcPath(emitter, target);
            var vector2Ints = coordPath as Vector2Int[] ?? coordPath.ToArray();
            if (!vector2Ints.Any())
            {
                Debug.LogWarning("Current field has no path from Emitter or Traget.");
                _controller.Lose(GetResult());
                return;
            }

            _path = new Vector3[vector2Ints.Length];
            for (var i = 0; i < vector2Ints.Length; ++i)
            {
                _controller.DebugMarkCell(vector2Ints[i]);
                _path[i] = _controller.Coord2World(vector2Ints[i]);
            }

            var cellSize =
                (Mathf.Abs((_controller.Coord2World(Vector2Int.zero) - _controller.Coord2World(Vector2Int.right)).x)
                 + Mathf.Abs((_controller.Coord2World(Vector2Int.zero) - _controller.Coord2World(Vector2Int.up)).z))
                * 0.5f;
            fm.Cells.ForEach(cell =>
            {
                var towerModel = cell.Item as ITower;
                if (towerModel == null) return;
                _towers.Add(new TowerLogic(towerModel, _controller.Coord2World(cell.Coordinate),
                    cellSize, _spawnedEnemies, this));
            });

            _waveIterator = fm.Waves.GetEnumerator();
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
            var enemy = new EnemyLogic(EnemiesLibrary.GetEnemyByType(type), _path, this);
            _spawnedEnemies.Add(enemy);
            _controller.AddEnemy(enemy);
        }

        /// <summary>
        /// Добавить выстрел
        /// </summary>
        /// <param name="shot">Добавляемый выстрел.</param>
        public void Shoot(ShotLogic shot)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Враг умер.
        /// </summary>
        /// <param name="enemy">Умерший враг.</param>
        public void EnemyDie(EnemyLogic enemy)
        {
            if (_spawnedEnemies.Contains(enemy))
            {
                _spawnedEnemies.Remove(enemy);
            }
            else
            {
                Debug.LogWarning("Enemy already was destroyed.");
            }

            if (_targetHealth > 0 && !_spawnedEnemies.Any() && !_workingWaves.Any() && _waveIterator == null)
            {
                // Закончились враги и волны.
                _controller.Win(GetResult());
            }
        }

        /// <summary>
        /// Враг достиг цели.
        /// </summary>
        /// <param name="hitPoints">Урон.</param>
        public void HitTarget(int hitPoints)
        {
            _targetHealth -= hitPoints;
            Debug.LogFormat("--- Hit by {0} points, health became {1}.", hitPoints, _targetHealth);

            if (_targetHealth <= 0)
            {
                _controller.Lose(GetResult());
            }
        }

        private IEnumerator StartWaveCoroutine(float delay, WaveModel wave)
        {
            yield return new WaitForSeconds(delay);
            StartWave(wave);
        }

        private void StartWave(WaveModel wave)
        {
            Debug.Log("+++ Start enemy wave!");
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
            _towers.ForEach(tower => tower.Update(deltaTime));
        }
    }
}