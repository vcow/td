using System;
using System.Xml.Serialization;
using Models.Enemies;

namespace Models
{
    public class FieldSerializer : XmlSerializer
    {
        public FieldSerializer() : base(typeof(FieldModel), new Type[]
        {
            typeof(CellModel),
            typeof(WaveModel),
            typeof(ItemType),
            typeof(EnemyType),
            typeof(WaveEnemyEntry)
        })
        {
        }
    }
}