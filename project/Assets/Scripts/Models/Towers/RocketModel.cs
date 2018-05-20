﻿namespace Models.Towers
{
    public class RocketModel : IWeapon
    {
        public int Radius
        {
            get { return 6; }
        }
        public int Damage
        {
            get { return 8; }
        }
        public float RechargeTime
        {
            get { return 3f; }
        }
        public float BulletSpeed
        {
            get { return 20f; }
        }
    }
}