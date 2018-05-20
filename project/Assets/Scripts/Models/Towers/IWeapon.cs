namespace Models.Towers
{
    public interface IWeapon
    {
        int Radius { get; }
        int Damage { get; }
        float RechargeTime { get; }
        float BulletSpeed { get; }
    }
}