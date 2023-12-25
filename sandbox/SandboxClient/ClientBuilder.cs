using SandboxClient.Model;

namespace SandboxClient;

public class ClientBuilder
{
    private string _requestFile = "";
    private string _timelineFile = "";

    private Client _client = new();

    public ClientBuilder AddConfig(Config config)
    {
        _requestFile = config.RequestFile;
        _timelineFile = config.TimelineFile;
        return this;
    }

    public Client Build()
    {
        return _client;
    }
}
