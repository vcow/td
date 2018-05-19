namespace Models.Towers
{
    public class TinnyTowerModel : ITower
    {
        public string Name
        {
            get { return "Tinny Tower"; }
        }

        public float BuyPrice
        {
            get { return 0; }
        }
        
        public float SellPrice
        {
            get { return 0; }
        }

        public IWeapon Weapon
        {
            get { return null; }
        }
    }
}