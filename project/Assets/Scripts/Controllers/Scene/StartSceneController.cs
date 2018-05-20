using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.Scene
{
    public class StartSceneController : SceneControllerBase
    {
        private IEnumerator<string> _pathIterator;
        private readonly List<FieldModel> _levels = new List<FieldModel>();

        [SerializeField] private Button _playButton;
        [SerializeField] private Transform _menuContainer;
        [SerializeField] private GameObject _levelButtonPrefab;

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

            _pathIterator = GameModel.Instance.GameSettings.LevelAssetNames.AsQueryable().GetEnumerator();
            LoadLevel();
        }

        private void OnDestroy()
        {
            if (!IsInitialized) return;
            
            _playButton.onClick.RemoveListener(OnPlay);
            
            foreach (var button in _menuContainer.GetComponentsInChildren<Button>())
            {
                button.onClick.RemoveAllListeners();
            }
        }

        private void OnPlay()
        {
            // TODO: Save start settings

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(EditorSceneName, LoadSceneMode.Single);
        }

        private void LoadLevel()
        {
            if (_pathIterator.MoveNext())
            {
                StartCoroutine(LoadLevelCoroutine(_pathIterator.Current));
            }
            else
            {
                _pathIterator.Dispose();
                _pathIterator = null;

                CreateMenu();
            }
        }

        private IEnumerator LoadLevelCoroutine(string path)
        {
            var levelPath = Path.Combine(Application.streamingAssetsPath, path);
            if (!levelPath.Contains(@"://"))
            {
                levelPath = string.Format(@"file://{0}", levelPath);
            }

            var www = UnityWebRequest.Get(levelPath);
            yield return www.SendWebRequest();

            if (string.IsNullOrEmpty(www.error))
            {
                var serializer = new FieldSerializer();
                using (var reader = new StringReader(www.downloadHandler.text))
                {
                    _levels.Add(serializer.Deserialize(reader) as FieldModel);
                }
            }
            else
            {
                Debug.LogWarningFormat("Failed to load level from: {0}", levelPath);
            }

            www.Dispose();
            LoadLevel();
        }

        private void CreateMenu()
        {
            Assert.IsNotNull(_menuContainer);
            Assert.IsNotNull(_levelButtonPrefab);

            _levels.ForEach(field =>
            {
                if (field == null) return;
                var bn = Instantiate(_levelButtonPrefab, _menuContainer);
                var button = bn.GetComponent<Button>();
                Assert.IsNotNull(button);
                ApplyButton(button, field);
            });
        }
        
        private void ApplyButton(Button button, FieldModel field)
        {
            var label = button.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.text = field.Name;
            }

            button.onClick.AddListener(() => GameModel.Instance.FieldModel = field);
        }
    }
}