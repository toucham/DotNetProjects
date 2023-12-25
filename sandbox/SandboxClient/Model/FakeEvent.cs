using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SandboxClient.Model;

public enum EventType
{
    Parallel,
    Single,
    None
}

[JsonConverter(typeof(FakeEventJsonConverter))]
public class FakeEvent
{
    public EventType EventType = EventType.None;
    public List<string> Ids = new();

    public FakeEvent(IList<string> ids)
    {
        Ids.AddRange(ids);
        SetEventType();
    }

    /// <summary>
    /// Set a new list of IDs of events to this fake event
    /// </summary>
    /// <param name="ids"></param>
    public void SetIds(IEnumerable<string> ids)
    {
        if (!ids.Any())
        {
            throw new Exception("ids is not found");
        }
        Ids = new();
        Ids.AddRange(ids);
        SetEventType();
    }

    public void SetIds(string id, int amount)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("Id is not found");
        }
        else if (amount <= 0)
        {
            throw new Exception($"amount must be more than 0: {0}");
        }
        Ids = new();
        for (int i = 0; i < amount; i++)
        {
            Ids.Add(id);
        }
        SetEventType();
    }

    private void SetEventType()
    {
        if (Ids.Count > 1)
        {
            EventType = EventType.Parallel;
        }
        else
        {
            EventType = EventType.Single;
        }
    }
}
