using System.Collections.Generic;
using Newtonsoft.Json;

namespace SandboxClient.Model;

public class ParsedInputFile
{
    IList<FakeRequest> Requests { get; set; } = new List<FakeRequest>();

    [JsonConverter(typeof(TimelineJsonConverter))]
    IList<Event> Timeline { get; set; } = new List<Event>();
}
