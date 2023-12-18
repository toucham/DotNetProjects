namespace SandboxServer.Models;

public enum ItemType
{
    Furniture,
    Appliance,
    Consumable,
}

public interface IItem
{
}

public class Item : IItem
{
    private string _Id { get; }

    public ItemType Type;

    public Item(string id)
    {
        _Id = id;
    }
}