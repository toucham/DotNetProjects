using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SandboxClient.Model;

namespace SandboxClient;

public class FakeEventJsonConverter : JsonConverter
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(string)
            || typeToConvert == typeof(Array)
            || typeToConvert == typeof(object);
    }

    public override FakeEvent? ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer
    )
    {
        JToken token = JToken.Load(reader);
        return new FakeEvent(ParseJToken(token));
    }

    private IList<string> ParseJToken(JToken token)
    {
        var idList = new List<string>();
        switch (token.Type)
        {
            case JTokenType.Array:
                var tokenArr = (JArray)token;
                foreach (JToken jToken in tokenArr)
                {
                    idList.AddRange(ParseJToken(jToken));
                }
                break;
            case JTokenType.Object:
                var tokenObj = (JObject)token;
                var amount = 0;
                var id = "";
                foreach (JProperty child in tokenObj.Properties())
                {
                    switch (child.Name)
                    {
                        case "id":
                            id = child.Value.ToString();
                            break;
                        case "amount":
                            amount = child.Value.Value<int>();
                            break;
                        default:
                            throw new Exception("unrecognized property");
                    }
                }
                if (!string.IsNullOrEmpty(id) && amount > 0)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        idList.Add(id);
                    }
                }
                else
                {
                    throw new Exception(
                        $"Id or Amount is not in the correct format: Id: {id}, Amount: {amount} "
                    );
                }
                break;
            case JTokenType.String:
                id = token.ToString();
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("id is null/emtpy");
                }
                idList.Add(id);
                break;
            default:
                throw new NotImplementedException();
        }
        return idList;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override bool CanWrite => false;
}
