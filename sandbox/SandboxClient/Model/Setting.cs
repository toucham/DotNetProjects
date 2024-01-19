namespace SandboxClient.Model;

public class WebServerSetting
{
    public string Url = "localhost";
    public string Port = "8080";
    public int MaxConcurrentRequests = 8;
}

public class Setting
{
    public string RequestsFile = "requestFile.json";
    public string EventsFile = "eventFile.json";
    public WebServerSetting WebServer = new();
}
