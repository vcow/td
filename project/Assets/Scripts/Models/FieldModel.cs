using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Models
{
    [XmlRoot("Field")]
    [XmlInclude(typeof(Cell))]
    public class FieldModel
    {
        [XmlElement] public Vector2Int Size;
        [XmlArray] public List<Cell> Cells = new List<Cell>();
    }

    [XmlType]
    [XmlInclude(typeof(RockModel))]
    [XmlInclude(typeof(TinnyTower))]
    [XmlInclude(typeof(SmallTower))]
    [XmlInclude(typeof(MediumTower))]
    [XmlInclude(typeof(LargeTower))]
    [XmlInclude(typeof(HugeTower))]
    public class Cell
    {
        [XmlElement] public Vector2Int Position;
        [XmlElement] public IItem Item;
    }

    public interface IWeapon
    {
        float Radius { get; }
        float Damage { get; }
        float RechargeTime { get; }
        float BulletSpeed { get; }
    }

    public interface IArmor
    {
        // TODO: Reserved, not implemented.
    }

    public interface IItem
    {
        float BuyPrice { get; }
        float SellPrice { get; }
        IEnumerable<IWeapon> Weapons { get; }
        IEnumerable<IArmor> Armors { get; }
    }
}