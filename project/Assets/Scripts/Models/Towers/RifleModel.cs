namespace Models.Towers
{
    public class RifleModel : IWeapon
    {
        public int Radius
        {
            get { return 3; }
        }
        public int Damage
        {
            get { return 2; }
        }
        public float RechargeTime
        {
            get { return 1f; }
        }
        public float BulletSpeed
        {
            get { return 50f; }
        }
    }
}