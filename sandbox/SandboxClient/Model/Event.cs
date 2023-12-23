using System.Collections.Generic;

namespace SandboxClient.Model;

public enum EventType
{
    Parallel,
    Single
}

public class Event
{
    public EventType Type { get; }
    public IList<string> Ids;

    public Event(IList<string> ids)
    {
        this.Ids = ids;
        if (ids.Count > 1)
        {
            Type = EventType.Parallel;
        }
        else
        {
            Type = EventType.Single;
        }
    }
}
