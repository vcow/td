using UnityEngine;

namespace Controllers.Scene
{
    public class EditorSceneController : SceneControllerBase
    {
        protected override void Start()
        {
            base.Start();
            if (!IsInitialized) return;

            InitScene();
        }

        protected override void InitScene()
        {
            // TODO: 
        }

        public void OnClickField(Vector2 value)
        {
            Debug.Log(value);
        }
    }
}