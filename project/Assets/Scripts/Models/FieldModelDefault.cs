using UnityEngine;

namespace Models
{
    public class FieldModelDefault : FieldModel
    {
        private static int _fieldCtr = 1;

        public FieldModelDefault()
        {
            var gs = GameModel.Instance.GameSettings;
            Name = string.Format("Field {0}", _fieldCtr++);
            Size = new Vector2Int(20, 20);
            TargetHealth = gs.TargetHealth;
            for (var i = 0; i < gs.NumberOfWaves; ++i)
            {
                Waves.Add(new WaveModelDefault());
            }
        }
    }
}