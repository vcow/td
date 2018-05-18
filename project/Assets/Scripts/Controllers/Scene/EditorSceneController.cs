using System;
using System.IO;
using System.Xml.Serialization;
using Models;
using UnityEngine;

namespace Controllers.Scene
{
    public class EditorSceneController : SceneControllerBase
    {
        private IItem _selectedItem;
        
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
            if (_selectedItem == null) return;
            
/*            Debug.Log(value);

            var f = new FieldModel {Size = new Vector2Int(30, 30)};
            f.Cells.Add(new Cell() {ItemType = ItemType.Rock, Position = new Vector2Int(0, 0)});
            f.Cells.Add(new Cell() {ItemType = ItemType.TinnyTower, Position = new Vector2Int(1, 0)});
            f.Cells.Add(new Cell() {ItemType = ItemType.TinnyTower, Position = new Vector2Int(2, 0)});
            f.Cells.Add(new Cell() {ItemType = ItemType.SmallTower, Position = new Vector2Int(2, 2)});

            var s = new XmlSerializer(typeof(FieldModel),
                new Type[]
                {
                    typeof(Cell),
                    typeof(ItemType)
                });

            var fs = new FileStream("C:/Work/research/location1.xml", FileMode.Create);
            s.Serialize(fs, f);
            fs.Close(); */
        }
    }
}