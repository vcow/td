namespace Models.Towers
{
    public interface IWeapon
    {
        float Radius { get; }
        float Damage { get; }
        float RechargeTime { get; }
        float BulletSpeed { get; }
    }
}