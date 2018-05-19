namespace Models.Towers
{
    public class TinnyTowerModel : ITower
    {
        public ItemType Type
        {
            get { return ItemType.TinnyTower; }
        }

        public string Name
        {
            get { return "Tinny Tower"; }
        }

        public decimal BuyPrice
        {
            get { return 50; }
        }
        
        public decimal SellPrice
        {
            get { return 25; }
        }

        public IWeapon Weapon
        {
            get { return null; }
        }
    }
}