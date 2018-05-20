using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Models
{
    [XmlRoot("Field")]
    [XmlInclude(typeof(CellModel))]
    [XmlInclude(typeof(WaveModel))]
    public class FieldModel : ICloneable
    {
        // TODO: Do not forget modify Clone()!
        
        [XmlElement] public string Name { get; set; }
        [XmlElement] public int TargetHealth { get; set; }
        [XmlElement] public Vector2Int Size { get; set; }
        [XmlArray] public List<CellModel> Cells = new List<CellModel>();
        [XmlArray] public List<WaveModel> Waves = new List<WaveModel>();

        object ICloneable.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// Глубокое клонирование модели.
        /// </summary>
        /// <returns>Клон.</returns>
        public FieldModel Clone()
        {
            var cells = new List<CellModel>(Cells.Count);
            var waves = new List<WaveModel>(Waves.Count);
            Cells.ForEach(cell => cells.Add(cell.Clone()));
            Waves.ForEach(wave => waves.Add(wave.Clone()));
            return new FieldModel {Name = Name, Size = Size, TargetHealth = TargetHealth, Cells = cells, Waves = waves};
        }

        /// <summary>
        /// Получить ячейку по указанной координате.
        /// </summary>
        /// <param name="coord">Координата.</param>
        /// <returns>Ячейка, <code>null</code>, если для координаты ячейка не задана.</returns>
        public CellModel GetCellByCoord(Vector2Int coord)
        {
            return Cells.SingleOrDefault(cell => cell.Coordinate == coord);
        }

        /// <summary>
        /// Удалить указанную ячейку.
        /// </summary>
        /// <param name="cell">Удаляема ячейка.</param>
        /// <returns>Возвращает <code>true</code>, если удаление успещно завершено.</returns>
        public bool ClearCell(CellModel cell)
        {
            if (!Cells.Contains(cell)) return false;

            Cells.Remove(cell);
            return true;
        }

        /// <summary>
        /// Добавляет ячейку.
        /// </summary>
        /// <param name="cell">Добавляемая ячейка.</param>
        public void AddCell(CellModel cell)
        {
            var c = GetCellByCoord(cell.Coordinate);
            if (c != null)
            {
                Debug.LogWarning("Cell for coordinate {0} already exists.");
                ClearCell(c);
            }

            Cells.Add(cell);
        }
    }
}