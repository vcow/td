using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Models
{
    [XmlType]
    public enum ItemType
    {
        [XmlEnum] Rock,
        [XmlEnum] TinnyTower,
        [XmlEnum] SmallTower,
        [XmlEnum] MediumTower,
        [XmlEnum] LargeTower,
        [XmlEnum] HugeTower,
        [XmlEnum] Emitter,
        [XmlEnum] Target
    }
    
    [XmlType]
    [XmlInclude(typeof(ItemType))]
    public class Cell : ICloneable
    {
        private ItemType _itemType = ItemType.Rock;
        private IItem _item = FieldItemsLibrary.Rock;

        [XmlElement] public Vector2Int Position { get; set; }

        [XmlElement]
        public ItemType ItemType
        {
            get { return _itemType; }
            set
            {
                if (value == _itemType) return;
                switch (value)
                {
                    case ItemType.Rock:
                        _item = FieldItemsLibrary.Rock;
                        break;
                    case ItemType.TinnyTower:
                        _item = FieldItemsLibrary.TinyTower;
                        break;
                    case ItemType.SmallTower:
                        _item = FieldItemsLibrary.SmallTower;
                        break;
                    case ItemType.MediumTower:
                        _item = FieldItemsLibrary.MediumTower;
                        break;
                    case ItemType.LargeTower:
                        _item = FieldItemsLibrary.LargeTower;
                        break;
                    case ItemType.HugeTower:
                        _item = FieldItemsLibrary.HugeTower;
                        break;
                    case ItemType.Emitter:
                        _item = FieldItemsLibrary.Emitter;
                        break;
                    case ItemType.Target:
                        _item = FieldItemsLibrary.Target;
                        break;
                    default:
                        throw new NotSupportedException();
                }

                _itemType = value;
            }
        }

        public IItem Item
        {
            get { return _item; }
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public Cell Clone()
        {
            return new Cell {ItemType = ItemType, Position = Position};
        }
    }
}