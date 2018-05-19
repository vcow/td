namespace Models.Towers
{
    public class MediumTowerModel : ITower
    {
        public ItemType Type
        {
            get { return ItemType.MediumTower; }
        }

        public string Name
        {
            get { return "Medium Tower"; }
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