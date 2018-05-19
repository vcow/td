using System;
using System.IO;
using System.Xml.Serialization;
using Settings;
using UnityEngine;

namespace Models
{
    public class GameModel
    {
        public FieldModel FieldModel;
        
        
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

            FieldModel = new FieldModel {Size = new Vector2Int(20, 20)};
/*
            Field.Cells.Add(new Cell() {ItemType = ItemType.Rock, Position = new Vector2Int(0, 0)});
            Field.Cells.Add(new Cell() {ItemType = ItemType.TinnyTower, Position = new Vector2Int(1, 0)});
            Field.Cells.Add(new Cell() {ItemType = ItemType.TinnyTower, Position = new Vector2Int(2, 0)});
            Field.Cells.Add(new Cell() {ItemType = ItemType.SmallTower, Position = new Vector2Int(2, 2)});

            var s = new XmlSerializer(typeof(FieldModel),
                new Type[]
                {
                    typeof(Cell),
                    typeof(ItemType)
                });

            var fs = new FileStream("C:/Work/research/location1.xml", FileMode.Create);
            s.Serialize(fs, Field);
            fs.Close();
*/
        }
    }
}