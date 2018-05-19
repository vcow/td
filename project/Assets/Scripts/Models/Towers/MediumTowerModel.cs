namespace Models.Towers
{
    public class MediumTowerModel : ITower
    {
        public string Name
        {
            get { return "Medium Tower"; }
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