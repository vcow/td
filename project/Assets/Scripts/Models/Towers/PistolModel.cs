namespace Models.Towers
{
    public class PistolModel : IWeapon
    {
        public int Radius
        {
            get { return 2; }
        }
        public int Damage
        {
            get { return 1; }
        }
        public float RechargeTime
        {
            get { return 0.5f; }
        }
        public float BulletSpeed
        {
            get { return 5f; }
        }
    }
}