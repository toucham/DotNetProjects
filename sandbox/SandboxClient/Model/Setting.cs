namespace SandboxClient.Model;

public class WebServerSetting
{
    public string Url { get; set; } = "127.0.0.1";
    public int? Port { get; set; }
    public int MaxConcurrentRequests { get; set; } = 8;

    public string BaseUrl { get => Port != null ? Url + ":" + Port.ToString() : Url; }
}

public class Setting
{
    public string RequestsFile { get; set; } = "requestFile.json";
    public string EventsFile { get; set; } = "eventFile.json";
    public WebServerSetting WebServer { get; set; } = new();
}
