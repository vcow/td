using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Scene
{
    public class GameSceneController : FieldSceneControllerBase
    {
        [SerializeField] private Text _moneyText;

        public const string SceneName = "GameScene";
        
        protected override void Start()
        {
            base.Start();
            if (!IsInitialized) return;

            InitScene();
        }

        protected override void InitScene()
        {
            base.InitScene();

            if (_moneyText != null)
            {
                var gm = GameModel.Instance;
                gm.MoneyChangedEvent += OnMoneyChanged;
                OnMoneyChanged(gm.Money);
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
    }
}