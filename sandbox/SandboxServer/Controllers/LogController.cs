using Microsoft.AspNetCore.Mvc;

namespace SandboxServer.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController : ControllerBase
{
    private readonly ILogger<LogController> _logger;

    public LogController(ILogger<LogController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "LogInfoMessage")]
    public void PostLogInfoMessage(string msg)
    {
        _logger.LogInformation(msg);
    }
}
