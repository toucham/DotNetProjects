using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SandboxClient.Model;

namespace SandboxClient;

public class ClientBuilder
{
    private Setting _setting = new();
    private readonly SocketsHttpHandler _httpHandler = new();
    private readonly ILogger<ClientBuilder> _logger;

    public ClientBuilder()
    {
        using var factory = LoggerFactory.Create(static builder =>
        {
            builder.AddConsole();
        });
        _logger = factory.CreateLogger<ClientBuilder>();
    }

    public ClientBuilder AddSetting(Setting setting)
    {
        _setting = setting;

        // set the maximum number of worker and completion port threads + socket connections
        var wantMaxConnect = setting.WebServer.MaxConcurrentRequests;
        ThreadPool.GetMaxThreads(out var workerThreads, out var completionPortThreads);
        _logger.LogInformation($"Get max threads: (worker threads: {workerThreads}, i/o port threads: {completionPortThreads})");
        var currentThreads = workerThreads + completionPortThreads;
        if (wantMaxConnect > currentThreads)
        {
            _logger.LogWarning(
                $"Need more threads to run the simulation: ({currentThreads})"
            );
            if (!ThreadPool.SetMaxThreads(wantMaxConnect, wantMaxConnect))
            {
                throw new Exception("Unable to set maximum threads to run the client");
            }
        }
        _httpHandler.MaxConnectionsPerServer = wantMaxConnect;

        return this;
    }

    public Client Build()
    {
        var (requests, options) = BuildFakeRequests();
        if (options != null)
        {
            _logger.LogInformation("FakeRequestOptions are applied...");
        }
        var events = BuildFakeEvents();
        var client = new Client(_httpHandler, requests, events, _setting.WebServer);
        return client;
    }

    private (Dictionary<string, FakeRequest>, FakeRequestOptions?) BuildFakeRequests()
    {
        var requests = new Dictionary<string, FakeRequest>();
        var reqJson = File.ReadAllText(_setting.RequestsFile);
        var fakeReqs = JsonConvert.DeserializeObject<FakeRequestJson>(reqJson) ?? throw new Exception("DeserializeObject as null");

        var reqs = fakeReqs.Requests;
        if (reqs == null)
        {
            throw new Exception("Unable to deserialize Requests");
        }
        else if (reqs.Count == 0)
        {
            throw new Exception("No fake requests in the list");
        }
        reqs.ForEach((req) =>
        {
            fakeReqs.Options?.Apply(req);
            requests.Add(req.Id, req);
        }
            );
        return (requests, fakeReqs.Options);
    }

    private IList<FakeEvent> BuildFakeEvents()
    {
        var eventJson = File.ReadAllText(_setting.EventsFile);
        var events = JsonConvert.DeserializeObject<IList<FakeEvent>>(eventJson);
        if (events == null)
            throw new Exception("Unable to deserialize Events");
        return events;
    }
}
