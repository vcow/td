namespace Models.Towers
{
    public class SmallTowerModel : ITower
    {
        public ItemType Type
        {
            get { return ItemType.SmallTower; }
        }

        public string Name
        {
            get { return "Small Tower"; }
        }

        public decimal BuyPrice
        {
            get { return 100; }
        }
        
        public decimal SellPrice
        {
            get { return 50; }
        }

        public IWeapon Weapon
        {
            get { return null; }
        }
    }
}