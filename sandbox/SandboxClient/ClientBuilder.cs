namespace SandboxClient;

public class ClientBuilder
{
    private string _filename = "input.json";

    private Client _client = new();

    public ClientBuilder AddFileName(string filename)
    {
        if (!string.IsNullOrEmpty(filename))
        {
            _filename = filename;
        }
        return this;
    }

    public ClientBuilder Parse()
    {
        return this;
    }

    public Client Build()
    {
        return _client;
    }
}
