namespace Models.Towers
{
    public class RocketModel : IWeapon
    {
        public int Radius
        {
            get { return 6; }
        }
        public int Damage
        {
            get { return 20; }
        }
        public float RechargeTime
        {
            get { return 4f; }
        }
        public float BulletSpeed
        {
            get { return 2f; }
        }
    }
}