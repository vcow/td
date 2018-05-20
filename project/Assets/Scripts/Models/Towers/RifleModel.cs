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
            get { return 4; }
        }
        public float RechargeTime
        {
            get { return 1.5f; }
        }
        public float BulletSpeed
        {
            get { return 8f; }
        }
    }
}