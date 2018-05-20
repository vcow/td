namespace Models.Towers
{
    public class CanonModel : IWeapon
    {
        public int Radius
        {
            get { return 4; }
        }
        public int Damage
        {
            get { return 5; }
        }
        public float RechargeTime
        {
            get { return 1.5f; }
        }
        public float BulletSpeed
        {
            get { return 20f; }
        }
    }
}