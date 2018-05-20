using AI;
using Models;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.Scene
{
    public class GameSceneController : FieldSceneControllerBase , IGameController
    {
        private GameLogic _gameLogic;
        private bool _isPaused;
        
        [SerializeField] private Text _moneyText;
        [SerializeField] private Text _messageText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Transform _menu;

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
            
            Assert.IsNotNull(_menu);
            Assert.IsNotNull(_messageText);
            _menu.gameObject.SetActive(false);
            
            Assert.IsNotNull(_nextButton);
            _nextButton.onClick.AddListener(OnNext);
        }

        private void OnNext()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(EditorSceneController.SceneName, LoadSceneMode.Single);
        }

        private void Update()
        {
            if (_gameLogic != null && !_isPaused)
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
            GameModel.Instance.MoneyChangedEvent -= OnMoneyChanged;
        }

        /// <summary>
        /// Пауза, вызывается из AI.
        /// </summary>
        /// <param name="value">Флаг, указывающий состояние паузы.</param>
        public void Pause(bool value)
        {
            _isPaused = value;
        }

        /// <summary>
        /// Победа, вызывается из AI.
        /// <param name="result">Результат.</param>
        /// </summary>
        public void Win(GameResult result)
        {
            Pause(true);
            
            _menu.gameObject.SetActive(true);
            _messageText.text = "YOU WIN!";
        }

        /// <summary>
        /// Поражение, вызывается из AI.
        /// <param name="result">Результат.</param>
        /// </summary>
        public void Lose(GameResult result)
        {
            Pause(true);
            
            _menu.gameObject.SetActive(true);
            _messageText.text = "YOU LOSE!";
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

        /// <summary>
        /// Создать выстрел.
        /// </summary>
        /// <param name="shot">Выстрел.</param>
        public void AddShot(ShotLogic shot)
        {
            Field.InstantiateShot(shot);
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