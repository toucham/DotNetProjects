using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SandboxClient.Model;

namespace SandboxClient;

public class Client
{
    private HttpClient _client;
    private ILogger<Client> _logger;
    private Dictionary<string, FakeRequest> _requests;
    private IList<FakeEvent> _events;
    private readonly WebServerSetting _setting;

    public Client(
        SocketsHttpHandler httpHandler,
        Dictionary<string, FakeRequest> requests,
        IList<FakeEvent> events,
        WebServerSetting setting
    )
    {
        // setup http client
        _setting = setting;
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

    public async Task Start()
    {
        using (_logger.BeginScope("Starting Events"))
        {
            foreach (var fakeEvent in _events)
            {
                var fakeRequests = fakeEvent.Ids.Select(
                    (id) =>
                    {
                        if (_requests.TryGetValue(id, out var fakeRequest))
                        {
                            return fakeRequest;
                        }
                        else
                        {
                            throw new Exception($"No request of id ({id}) found");
                        }
                    }
                );
                switch (fakeEvent.EventType)
                {
                    case EventType.Parallel:
                        await SendParallelRequest(fakeRequests);
                        break;
                    case EventType.Single:
                        SendSingleRequest(fakeRequests.First());
                        break;
                    default:
                        throw new Exception($"Unidentified EventType: {fakeEvent.EventType}");
                }
            }
        }
    }

    private HttpResponseMessage SendSingleRequest(FakeRequest fakeReq)
    {
        using (_logger.BeginScope($"ID: {fakeReq.Id} to \"{fakeReq.Url}\""))
        {
            _logger.LogInformation($"Sending a single request of id {fakeReq.Id} to {fakeReq.Url}");
            var req = fakeReq.Convert(_setting.Url);
            var res = _client.Send(req);
            if (res.IsSuccessStatusCode)
            {
                _logger.LogInformation("");
            }
            else
            {
                _logger.LogWarning("");
            }
            return res;
        }
    }

    private async Task<HttpResponseMessage[]> SendParallelRequest(
        IEnumerable<FakeRequest> fakeRequests
    )
    {
        List<Task<HttpResponseMessage>> allTasks = new();
        using (_logger.BeginScope($"Sending parallel requests: {fakeRequests.Count()} requests"))
        {
            foreach (var fakeReq in fakeRequests)
            {
                allTasks.Add(SendAsyncRequest(fakeReq));
            }
            var responses = await Task.WhenAll(allTasks);
            _logger.LogInformation("Parallel requests are all sent");
            return responses;
        }
    }

    private async Task<HttpResponseMessage> SendAsyncRequest(FakeRequest fakeReq)
    {
        var req = fakeReq.Convert(_setting.Url);
        using (_logger.BeginScope($"ID: {fakeReq.Id} to \"{fakeReq.Url}\""))
        {
            var response = await _client.SendAsync(req);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("");
            }
            else
            {
                _logger.LogWarning("");
            }
            return response;
        }
    }
}
