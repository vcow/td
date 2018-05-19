namespace Models.Towers
{
    public class SmallTowerModel : ITower
    {
        public string Name
        {
            get { return "Small Tower"; }
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