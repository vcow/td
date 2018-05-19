using System.Xml.Serialization;

namespace Models.Enemies
{
    [XmlType]
    public enum EnemyType
    {
        [XmlEnum] Small,
        [XmlEnum] Medium,
        [XmlEnum] Large
    }

    public interface IEnemy
    {
        EnemyType Type { get; }
        int Health { get; }
        int Damage { get; }
        float Speed { get; }
        decimal Price { get; }
    }
}