namespace Models.Towers
{
    public class HugeTowerModel : ITower
    {
        public ItemType Type
        {
            get { return ItemType.HugeTower; }
        }

        public string Name
        {
            get { return "Huge Tower"; }
        }

        public decimal BuyPrice
        {
            get { return 300; }
        }

        public decimal SellPrice
        {
            get { return 150; }
        }

        public IWeapon Weapon
        {
            get { return WeaponsLibrary.Rocket; }
        }
    }
}