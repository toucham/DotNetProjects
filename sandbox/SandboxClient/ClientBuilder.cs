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
            builder
                .AddFilter("Microsft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("SandboxClient.ClientBuilder", LogLevel.Information)
                .AddFilter("SandboxClient.ClientBuilder", LogLevel.Debug)
                .AddFilter("SandboxClient.ClientBuilder", LogLevel.Error)
                .AddConsole();
        });
        _logger = factory.CreateLogger<ClientBuilder>();
    }

    public ClientBuilder AddSetting(Setting setting)
    {
        _setting = setting;

        // set the maximum number of worker and completion port threads + socket connections
        var maxConnect = setting.WebServer.MaxConcurrentRequests;
        ThreadPool.GetMaxThreads(out var workerThreads, out var completionPortThreads);
        var minThread =
            workerThreads > completionPortThreads ? completionPortThreads : workerThreads;
        if (maxConnect > minThread)
        {
            _logger.LogWarning(
                $"The set number of threads exceed the number of minimal threads ({minThread})"
            );
            maxConnect = minThread;
        }

        if (!ThreadPool.SetMaxThreads(maxConnect, maxConnect))
        {
            throw new Exception("Unable to set maximum threads");
        }
        _httpHandler.MaxConnectionsPerServer = maxConnect;

        return this;
    }

    public Client Build()
    {
        var requests = BuildFakeRequests();
        var events = BuildFakeEvents();
        var client = new Client(_httpHandler, requests, events, _setting.WebServer);
        return client;
    }

    private Dictionary<string, FakeRequest> BuildFakeRequests()
    {
        var requests = new Dictionary<string, FakeRequest>();
        var reqJsonData = File.ReadAllText(_setting.RequestFile);
        var reqs = JsonConvert.DeserializeObject<List<FakeRequest>>(reqJsonData);
        if (reqs == null)
        {
            throw new Exception("Unable to deserialize Requests");
        }
        reqs.ForEach((req) => requests.Add(req.Id, req));
        return requests;
    }

    private IList<FakeEvent> BuildFakeEvents()
    {
        var jsonEventData = File.ReadAllText(_setting.TimelineFile);
        var events = JsonConvert.DeserializeObject<IList<FakeEvent>>(jsonEventData);
        if (events == null)
            throw new Exception("Unable to deserialize Events");
        return events;
    }
}
