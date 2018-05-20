namespace Models.Towers
{
    public class PistolModel : IWeapon
    {
        public int Radius
        {
            get { return 1; }
        }
        public int Damage
        {
            get { return 1; }
        }
        public float RechargeTime
        {
            get { return 0.3f; }
        }
        public float BulletSpeed
        {
            get { return 30f; }
        }
    }
}