using AI;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Scene
{
    public class GameSceneController : FieldSceneControllerBase , IGameController
    {
        private GameLogic _gameLogic;
        
        [SerializeField] private Text _moneyText;

        public const string SceneName = "GameScene";
        
        protected override void InitScene()
        {
            base.InitScene();

            Field.MarkerIsVisible = false;

            if (_moneyText != null)
            {
                var gm = GameModel.Instance;
                gm.MoneyChangedEvent += OnMoneyChanged;
                OnMoneyChanged(gm.Money);
            }
            
            _gameLogic = new GameLogic(this);
            _gameLogic.Start();
        }

        private void Update()
        {
            if (_gameLogic != null)
            {
                _gameLogic.Update(Time.deltaTime);
            }
        }

        private void OnMoneyChanged(decimal value)
        {
            _moneyText.text = string.Format("money: {0}", value);
        }

        private void OnDestroy()
        {
            if (_moneyText != null)
            {
                GameModel.Instance.MoneyChangedEvent -= OnMoneyChanged;
            }
        }

        /// <summary>
        /// Пауза, вызывается из AI.
        /// </summary>
        /// <param name="value">Флаг, указывающий состояние паузы.</param>
        public void Pause(bool value)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Победа, вызывается из AI.
        /// <param name="result">Результат.</param>
        /// </summary>
        public void Win(GameResult result)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Поражение, вызывается из AI.
        /// <param name="result">Результат.</param>
        /// </summary>
        public void Lose(GameResult result)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Преобразует позицию на игровом поле в мировые координаты.
        /// </summary>
        /// <param name="coord">Позиция на игровом поле.</param>
        /// <returns>Мировые координаты.</returns>
        public Vector3 Coord2World(Vector2Int coord)
        {
            return Field.Pos2World(Coord2Pos(coord));
        }

        /// <summary>
        /// Создать врага.
        /// </summary>
        /// <param name="enemy">Логика создаваемого врага.</param>
        public void AddEnemy(EnemyLogic enemy)
        {
            Field.InstantiateEnemy(enemy);
        }

        void IGameController.DebugMarkCell(Vector2Int coord)
        {
            var marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            marker.transform.SetParent(Field.transform, false);
            marker.transform.localScale = Vector3.one * 0.1f;
            marker.transform.localPosition = Field.Pos2World(Coord2Pos(coord));
        }
    }
}