namespace Models
{
    public class TargetModel : IItem
    {
        public ItemType Type
        {
            get { return ItemType.Target; }
        }

        public string Name
        {
            get { return "Target"; }
        }
    }
}