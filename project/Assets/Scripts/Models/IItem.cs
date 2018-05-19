namespace Models
{
    public interface IItem
    {
        string Name { get; }
        ItemType Type { get; }
    }
}