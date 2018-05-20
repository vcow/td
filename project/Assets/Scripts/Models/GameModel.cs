using System;
using Settings;

namespace Models
{
    public class GameModel
    {
        private FieldModel _fieldModel;

        private static GameModel _instance;
        private GameSettings _gameSettings;

        private decimal _money;

        private GameModel()
        {
        }

        public decimal Money
        {
            get { return _money; }
            set
            {
                if (value == _money) return;
                _money = value;
                if (MoneyChangedEvent != null)
                {
                    MoneyChangedEvent.Invoke(_money);
                }
            }
        }

        public event Action<decimal> MoneyChangedEvent;

        /// <summary>
        /// Текущая выбраннная локация.
        /// </summary>
        public FieldModel FieldModel
        {
            get { return _fieldModel ?? (_fieldModel = new FieldModelDefault()); }
            set { _fieldModel = value; }
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
            // TODO: Initialize game settings dependent fields.

            Money = GameSettings.StartMoney;
        }
    }
}