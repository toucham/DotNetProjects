using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SandboxClient.Json;

namespace SandboxClient.Model;

public class FakeRequest
{
    [JsonProperty("id")]
    public required string Id;

    [JsonProperty("method")]
    public required string Method;

    [JsonProperty("header")]
    [JsonConverter(typeof(DictCaseInsensitiveJsonConverter))]
    public Dictionary<string, string>? Header;

    [JsonProperty("body")]
    public JToken? Body;

    [JsonProperty("path")]
    private string _path = "";
    public string Path
    {
        get => _path.Trim('/');
    }

    public Uri BuildUri(string baseUri, int? port)
    {
        if (port != null)
        {
            return new UriBuilder("http", baseUri, (int)port, _path).Uri;
        }
        else
        {
            return new UriBuilder("http", baseUri, 80, _path).Uri;
        }
    }
}
