using Models;
using Settings;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Controllers.Scene
{
    /// <summary>
    /// Базовый класс контроллера сцены. Переводит игру на стартовую сцену, сообщает модели игровые настройки.
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class SceneControllerBase : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;

        private const string StartSceneName = "StartScene";

        protected static bool IsInitialized { get; set; }

        private void Awake()
        {
            Assert.IsNotNull(_gameSettings);
            GameModel.Instance.GameSettings = _gameSettings;
        }

        protected virtual void Start()
        {
            if (IsInitialized) return;

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(StartSceneName, LoadSceneMode.Single);
        }

        protected static void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            if (scene.name != StartSceneName) return;

            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.SetActiveScene(scene);
        }

        protected abstract void InitScene();
    }
}