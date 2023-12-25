using System.Collections.Generic;
using SandboxClient.Model;

namespace SandboxClient;

public class Client
{
    public Dictionary<string, FakeRequest> Requests = new();

    public List<FakeEvent> Events = new();

    public void Start() { }
}
