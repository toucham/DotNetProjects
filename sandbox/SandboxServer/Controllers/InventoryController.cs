using Microsoft.AspNetCore.Mvc;
using SandboxServer.Models;
using SandboxServer.Services;

namespace SandboxServer.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly ILogger<InventoryController> _logger;
    private readonly IInventoryService _inventoryService;

    public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        _logger = logger;
    }

    [HttpGet(Name = "Item")]
    public IEnumerable<IItem> GetItems(IEnumerable<string> ids)
    {
        return _inventoryService.GetItems(ids);
    }

    [HttpPost(Name = "Item")]
    public bool PostItems()
    {
        return true;
    }


}