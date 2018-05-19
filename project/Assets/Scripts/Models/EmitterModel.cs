namespace Models
{
    public class EmitterModel : IItem
    {
        public ItemType Type
        {
            get { return ItemType.Emitter; }
        }

        public string Name
        {
            get { return "Enemy emitter"; }
        }
    }
}