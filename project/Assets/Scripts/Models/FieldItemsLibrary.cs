using System;
using System.Collections.Generic;
using Models.Towers;

namespace Models
{
    public static class FieldItemsLibrary
    {
        private static readonly Dictionary<ItemType, IItem> MapByType;

        public static readonly RockModel Rock;
        public static readonly TinnyTowerModel TinyTower;
        public static readonly SmallTowerModel SmallTower;
        public static readonly MediumTowerModel MediumTower;
        public static readonly LargeTowerModel LargeTower;
        public static readonly HugeTowerModel HugeTower;
        public static readonly EmitterModel Emitter;
        public static readonly TargetModel Target;

        static FieldItemsLibrary()
        {
            Rock = new RockModel();
            TinyTower = new TinnyTowerModel();
            SmallTower = new SmallTowerModel();
            MediumTower = new MediumTowerModel();
            LargeTower = new LargeTowerModel();
            HugeTower = new HugeTowerModel();
            Emitter = new EmitterModel();
            Target = new TargetModel();

            MapByType = new Dictionary<ItemType, IItem>
            {
                {ItemType.Rock, Rock},
                {ItemType.TinnyTower, TinyTower},
                {ItemType.SmallTower, SmallTower},
                {ItemType.MediumTower, MediumTower},
                {ItemType.LargeTower, LargeTower},
                {ItemType.HugeTower, HugeTower},
                {ItemType.Emitter, Emitter},
                {ItemType.Target, Target}
            };
        }

        public static IItem GetItemByType(ItemType type)
        {
            IItem item;
            if (MapByType.TryGetValue(type, out item)) return item;
            throw new NotSupportedException();
        }
    }
}