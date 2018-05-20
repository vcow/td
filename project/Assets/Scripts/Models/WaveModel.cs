using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Models.Enemies;

namespace Models
{
    [XmlType("Wave")]
    [XmlInclude(typeof(EnemyType))]
    [XmlInclude(typeof(WaveEnemyEntry))]
    public class WaveModel : ICloneable
    {
        // TODO: Do not forget modify Clone()!
        
        [XmlElement] public float EmissionSpeed { get; set; }
        [XmlElement] public List<WaveEnemyEntry> Enemies;

        object ICloneable.Clone()
        {
            return Clone();
        }

        public WaveModel Clone()
        {
            var enemies = new List<WaveEnemyEntry>();
            Enemies.ForEach(entry => enemies.Add(entry.Clone()));
            return new WaveModel {EmissionSpeed = EmissionSpeed, Enemies = enemies};
        }
    }

    [XmlType("EnemyEntry")]
    public class WaveEnemyEntry : ICloneable
    {
        [XmlElement] public EnemyType Type { get; set; }
        [XmlElement] public int EnemiesCount { get; set; }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public WaveEnemyEntry Clone()
        {
            return new WaveEnemyEntry {Type = Type, EnemiesCount = EnemiesCount};
        }
    }
}