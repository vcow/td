namespace Models.Towers
{
    public class LargeTowerModel : ITower
    {
        public ItemType Type
        {
            get { return ItemType.LargeTower; }
        }

        public string Name
        {
            get { return "Large Tower"; }
        }

        public decimal BuyPrice
        {
            get { return 0; }
        }
        
        public decimal SellPrice
        {
            get { return 0; }
        }

        public IWeapon Weapon
        {
            get { return null; }
        }
    }
}