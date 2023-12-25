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
    public Dictionary<string, FakeRequest> Requests;

    public IList<FakeEvent> Events;

    public Client()
    {
        Requests = new();
        Events = new List<FakeEvent>();
        // setup http client
        _client = new HttpClient();

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
    }

    public void Start() { }
}
