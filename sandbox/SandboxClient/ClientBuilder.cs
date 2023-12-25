namespace SandboxClient;

public class ClientBuilder
{
    private string _requestFile = "requests_input.json";
    private string _timelineFile = "timeline_input.json";

    private Client _client = new();

    public ClientBuilder AddRequestFile(string filename)
    {
        if (!string.IsNullOrEmpty(filename))
        {
            _requestFile = filename;
        }
        return this;
    }

    public ClientBuilder AddTimelineFile(string filename)
    {
        if (!string.IsNullOrEmpty(filename))
        {
            _timelineFile = filename;
        }
        return this;
    }

    public Client Build()
    {
        return _client;
    }
}
