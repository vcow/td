﻿namespace Models.Towers
{
    public class HugeTowerModel : ITower
    {
        public float BuyPrice
        {
            get { return 0; }
        }
        
        public float SellPrice
        {
            get { return 0; }
        }

        public IWeapon Weapon
        {
            get { return null; }
        }
    }
}