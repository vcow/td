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
    
    [XmlType("Cell")]
    [XmlInclude(typeof(ItemType))]
    public class CellModel : ICloneable
    {
        // TODO: Do not forget modify Clone()!
        
        private ItemType _itemType = ItemType.Rock;
        private IItem _item = FieldItemsLibrary.Rock;

        [XmlElement] public Vector2Int Coordinate { get; set; }

        [XmlElement]
        public ItemType ItemType
        {
            get { return _itemType; }
            set
            {
                if (value == _itemType) return;
                _itemType = value;
                _item = FieldItemsLibrary.GetItemByType(_itemType);
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

        public CellModel Clone()
        {
            return new CellModel {ItemType = ItemType, Coordinate = Coordinate};
        }
    }
}