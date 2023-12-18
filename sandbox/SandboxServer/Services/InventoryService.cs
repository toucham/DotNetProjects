using DICSharpDev.Lib;
using SandboxServer.Models;

namespace SandboxServer.Services;

public interface IInventoryService
{
    public bool StoreItems(IEnumerable<IItem> items);

    public IEnumerable<Item> GetItems(IEnumerable<string> ids);
}

[DILifetime(ServiceLifetime.Singleton)]
public class InventoryService : IInventoryService
{
    public InventoryService()
    {
    }

    public IEnumerable<Item> GetItems(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public bool StoreItems(IEnumerable<IItem> items)
    {
        throw new NotImplementedException();
    }
}