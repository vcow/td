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
            get { return 10; }
        }
        public float RechargeTime
        {
            get { return 3f; }
        }
        public float BulletSpeed
        {
            get { return 3f; }
        }
    }
}