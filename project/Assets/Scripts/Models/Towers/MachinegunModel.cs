namespace Models.Towers
{
    public class MachinegunModel : IWeapon
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
            get { return 0.2f; }
        }
        public float BulletSpeed
        {
            get { return 7f; }
        }
    }
}