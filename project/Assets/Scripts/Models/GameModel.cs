using Settings;

namespace Controllers
{
    public class GameModel
    {
        private static GameModel _instance;
        private GameSettings _gameSettings;

        private GameModel()
        {
        }
        
        public static GameModel Instance
        {
            get { return _instance ?? (_instance = new GameModel()); }
        }
        
        public GameSettings GameSettings
        {
            get { return _gameSettings; }
            set
            {
                if (value == _gameSettings) return;
                _gameSettings = value;
                ApplyGameSettings();
            }
        }

        private void ApplyGameSettings()
        {
            // TODO:
        }
    }
}