using System.Collections.Generic;

namespace Models.Towers
{
    public interface ITower : IItem
    {
        decimal BuyPrice { get; }
        decimal SellPrice { get; }
        IWeapon Weapon { get; }
    }
}