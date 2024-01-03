using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using SandboxClient.Model;

namespace SandboxClient;

public class ClientBuilder
{
    private string _requestFile = "";
    private string _eventsFile = "";
    private Setting _setting = new();
    private SocketsHttpHandler _httpHandler = new();

    public ClientBuilder AddSetting(Setting setting)
    {
        _setting = setting;
        #region Client config
        _requestFile = setting.RequestFile;
        _eventsFile = setting.TimelineFile;
        #endregion

        #region Web Server config
        _httpHandler.MaxConnectionsPerServer = setting.WebServer.MaxConcurrentRequests;
        #endregion

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
        var reqJsonData = File.ReadAllText(_requestFile);
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
        var jsonEventData = File.ReadAllText(_eventsFile);
        var events = JsonConvert.DeserializeObject<IList<FakeEvent>>(jsonEventData);
        if (events == null)
            throw new Exception("Unable to deserialize Events");
        return events;
    }
}
