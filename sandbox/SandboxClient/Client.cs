using System.Collections;
using System.Collections.Generic;
using SandboxClient.Model;

namespace SandboxClient;

public class Client
{
    public Dictionary<string, ParsedInputFile> Requests = new();

    public IEnumerable<IEnumerable<string>> Timeline = new List<List<string>>();

    public void Start() { }
}
