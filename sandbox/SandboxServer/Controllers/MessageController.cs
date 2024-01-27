using Microsoft.AspNetCore.Mvc;
using SandboxServer.Models;
using SandboxServer.Services.Interfaces;

namespace SandboxServer.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<ActionResult> PostMessage()
    {
        using StreamReader reader = new(Request.Body, false);
        var body = await reader.ReadToEndAsync();
        if (body == null)
        {
            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError, Content = "Null from StreamReader" };
        }
        _messageService.Add(body);
        return Ok();
    }

    [HttpPost]
    [Consumes("application/json")]
    public ActionResult PostMessageJson([FromBody] Message msg)
    {
        _messageService.Add(msg);
        return Ok();
    }
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public ActionResult<Message> GetMessage([FromQuery] int? index)
    {
        if (index == null)
        {
            return BadRequest();
        }
        if (index >= _messageService.Count)
        {
            return new ContentResult { StatusCode = StatusCodes.Status404NotFound, Content = $"Invalid Index. There is only {_messageService.Count} messages" };
        }
        return _messageService.Get((int)index);
    }

    [HttpGet]
    public ActionResult<IList<Message>> GetMessages()
    {
        return _messageService.GetAll().ToList();
    }

    [HttpDelete]
    public ActionResult<Message> DeleteMsg([FromBody] int index)
    {
        var msg = _messageService.Remove(index);
        if (msg == null)
        {
            return new ContentResult { StatusCode = StatusCodes.Status404NotFound, Content = "Invalid index" };
        }
        return msg;
    }
}
