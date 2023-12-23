using System;
using Newtonsoft.Json;

namespace SandboxClient;

public class TimelineJsonConverter : JsonConverter
{
    public override bool CanConvert(Type typeToConvert)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer
    )
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override bool CanWrite => false;
}
