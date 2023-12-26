using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using SandboxClient.Model;

namespace SandboxClient;

public class Client
{
    private string _baseUrl;
    private HttpClient _client;
    private ILogger<Client> _logger;
    private Dictionary<string, FakeRequest> _requests;
    private IList<FakeEvent> _events;

    public Client(
        SocketsHttpHandler httpHandler,
        Dictionary<string, FakeRequest> requests,
        IList<FakeEvent> events
    )
    {
        // setup http client
        _client = new HttpClient(httpHandler);

        // setup logging
        using var factory = LoggerFactory.Create(static builder =>
        {
            builder
                .AddFilter("Microsft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("SandboxClient.Client", LogLevel.Information)
                .AddFilter("SandboxClient.Client", LogLevel.Debug)
                .AddFilter("SandboxClient.Client", LogLevel.Error)
                .AddConsole();
        });
        _logger = factory.CreateLogger<Client>();

        // instantiante requests and events
        _requests = requests;
        _events = events;
    }

    public void Start()
    {
        using (_logger.BeginScope("Starting Events")) { }
    }
}
