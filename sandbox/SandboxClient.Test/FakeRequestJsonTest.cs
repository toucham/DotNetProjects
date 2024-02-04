using Newtonsoft.Json;
using SandboxClient.Model;

namespace SandboxClient.Test;

[TestFixture]
public class FakeRequestJsonTest
{
    private static string FakeRequestJsonWithOption = """
    {
        "options": {
            "defaultBody": "string",
            "basePath": "/test/api",
        },
        "requests": [
            {
                "id": "1",
                "method": "POST",
                "path": "Post",
                "body": "hello this is a message"
            },
            {
                "id": "2",
                "method": "POST",
                "path": "PostJson",
                "body": {
                    "body": "one more message"
                }
            },
            {
                "id": "3",
                "method": "GET",
                "path": "Get?index=1"
            }
        ]
    }
    """;
    private static string FakeRequestJsonWithNoOption = """
    {
        "requests": [
            {
                "id": "1",
                "method": "POST",
                "path": "Post",
                "body": "hello this is a message"
            },
            {
                "id": "2",
                "method": "POST",
                "path": "PostJson",
                "body": {
                    "body": "one more message"
                }
            },
            {
                "id": "3",
                "method": "GET",
                "path": "Get?index=1"
            }
        ]
    }
    """;

    [Test]
    public void ParseJson_ShouldDeserialize()
    {
        var fakeReqJson = JsonConvert.DeserializeObject<FakeRequestJson>(FakeRequestJsonWithOption);
        Assert.That(fakeReqJson, Is.Not.Null);
        Assert.That(fakeReqJson, Has.Property("Options"));
        Assert.Multiple(() =>
        {
            Assert.That(fakeReqJson.Options?.DefaultBody?.ToString(), Is.EqualTo("string"));
            Assert.That(fakeReqJson.Options?.BasePath, Is.EqualTo("/test/api"));
        });
    }

    [Test]
    public void ParseJson_ShouldDeserialize_NoOption()
    {
        var fakeReqJson = JsonConvert.DeserializeObject<FakeRequestJson>(FakeRequestJsonWithNoOption);
        Assert.That(fakeReqJson, Is.Not.Null);
        Assert.That(fakeReqJson.Options, Is.Null);
    }
}