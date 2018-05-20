using System.Collections.Generic;
using Models.Enemies;

namespace Models
{
    public class WaveModelDefault : WaveModel
    {
        public WaveModelDefault()
        {
            var gs = GameModel.Instance.GameSettings;
            EmissionSpeed = gs.EmissionSpeed;
            Enemies = new List<WaveEnemyEntry>
            {
                new WaveEnemyEntry {Type = EnemyType.Small, EnemiesCount = gs.SmallEnemiesInWave},
                new WaveEnemyEntry {Type = EnemyType.Medium, EnemiesCount = gs.MediumEnemiesInWave},
                new WaveEnemyEntry {Type = EnemyType.Large, EnemiesCount = gs.LargeEnemiesInWave}
            };
        }
    }
}