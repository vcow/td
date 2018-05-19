namespace Models
{
    public class RockModel : IItem
    {
        public ItemType Type
        {
            get { return ItemType.Rock; }
        }

        public string Name
        {
            get { return "Rock"; }
        }
    }
}