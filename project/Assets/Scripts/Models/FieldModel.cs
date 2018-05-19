using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Models
{
    [XmlRoot("Field")]
    [XmlInclude(typeof(Cell))]
    public class FieldModel : ICloneable
    {
        [XmlElement] public int TargetLives { get; set; }
        [XmlElement] public Vector2Int Size { get; set; }
        [XmlArray] public List<Cell> Cells = new List<Cell>();

        object ICloneable.Clone()
        {
            return Clone();
        }

        public FieldModel Clone()
        {
            var cells = new List<Cell>(Cells.Count);
            Cells.ForEach(cell => cells.Add(cell.Clone()));
            return new FieldModel {Size = Size, TargetLives = TargetLives, Cells = cells};
        }
    }
}