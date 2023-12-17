using System.Collections.Generic;
using SandboxClient.Model;

namespace SandboxClient;

public class Client
{
    private Dictionary<string, FakeRequest> _requests = new Dictionary<string, FakeRequest>();

    public void Start() { }
}
