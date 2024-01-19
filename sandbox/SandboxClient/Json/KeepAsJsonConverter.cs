using System;
using Newtonsoft.Json;

namespace SandboxClient.Json;

public class KeepAsJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => true;

    public override string? ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer
    )
    {
        return reader.TokenType == JsonToken.Null ? null : reader.Value?.ToString();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
