using System;
using System.Collections.Generic;
using Models.Enemies;

namespace Models
{
    public static class EnemiesLibrary
    {
        private static readonly Dictionary<EnemyType, IEnemy> MapByType;
        
        public static readonly SmallEnemyModel SmallEnemy;
        public static readonly MediumEnemyModel MediumEnemy;
        public static readonly LargeEnemyModel LargeEnemy;

        static EnemiesLibrary()
        {
            SmallEnemy = new SmallEnemyModel();
            MediumEnemy = new MediumEnemyModel();
            LargeEnemy = new LargeEnemyModel();
            
            MapByType = new Dictionary<EnemyType, IEnemy>
            {
                {EnemyType.Small, SmallEnemy},
                {EnemyType.Medium, MediumEnemy},
                {EnemyType.Large, LargeEnemy},
            };
        }

        public static IEnemy GetEnemyByType(EnemyType type)
        {
            IEnemy enemy;
            if (MapByType.TryGetValue(type, out enemy)) return enemy;
            throw new NotSupportedException();
        }
    }
}