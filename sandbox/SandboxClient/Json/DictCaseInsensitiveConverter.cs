using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SandboxClient.Json;

public class DictCaseInsensitiveJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Dictionary<string, string>);
    }

    public override Dictionary<string, string> ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer
    )
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (reader.TokenType == JsonToken.StartObject)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var propName = reader.Value?.ToString();
                    if (string.IsNullOrEmpty(propName))
                    {
                        throw new Exception(
                            "Unable to parse JSON object - property name returns null"
                        );
                    }
                    if (reader.Read())
                    {
                        var propVal = reader.Value?.ToString();
                        if (string.IsNullOrEmpty(propVal))
                        {
                            throw new Exception(
                                "Unable to parse JSON object - property value returns null"
                            );
                        }
                        dict.Add(propName, propVal);
                    }
                }
                else if (reader.TokenType == JsonToken.EndObject)
                {
                    break;
                }
                else
                {
                    throw new Exception($"Incorrect JSON object format: {reader.TokenType}");
                }
            }
        }
        return dict;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
