namespace Models.Towers
{
    public class LargeTowerModel : ITower
    {
        public string Name
        {
            get { return "Large Tower"; }
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