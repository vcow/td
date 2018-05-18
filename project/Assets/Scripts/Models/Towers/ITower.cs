using System.Collections.Generic;

namespace Models.Towers
{
    public interface ITower : IItem
    {
        float BuyPrice { get; }
        float SellPrice { get; }
        IWeapon Weapon { get; }
    }
}