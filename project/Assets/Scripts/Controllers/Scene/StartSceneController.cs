using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.Scene
{
    public class StartSceneController : SceneControllerBase
    {
        [SerializeField] private Button _playButton;
        
        private const string EditorSceneName = "EditorScene";
        
        protected override void Start()
        {
            if (IsInitialized) return;

            InitScene();
            IsInitialized = true;
        }

        protected override void InitScene()
        {
            Assert.IsNotNull(_playButton);
            _playButton.onClick.AddListener(OnPlay);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlay);
        }

        private void OnPlay()
        {
            // TODO: Save start settings
            
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(EditorSceneName, LoadSceneMode.Single);
        }
    }
}