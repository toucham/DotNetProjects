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
    public required Dictionary<string, string> Header;

    [JsonProperty("body")]
    public JObject? Body;

    [JsonProperty("url")]
    public string? Url;
}
