namespace SandboxClient.Model;

public class WebServerConfig
{
    public string Url = "localhost";
    public string Port = "8080";
}

public class Config
{
    public string RequestFile = "requestFile.json";
    public string TimelineFile = "timelineFile.json";
    public WebServerConfig WebServer = new();
}
