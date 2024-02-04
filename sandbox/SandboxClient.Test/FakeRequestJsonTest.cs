using Newtonsoft.Json;
using SandboxClient.Model;

namespace SandboxClient.Test;

[TestFixture]
public class FakeRequestJsonTest
{
    private static string JsonMultiFakeRequestsCase = """
    [
        {
            "id": "1",
            "method": "post",
            "header": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache"
            },
            "body": {
                "status": "Ok",
                "cool": 13
            }
        },
        {
            "id": "2",
            "method": "post",
            "header": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache"
            },
            "body": "hi"
        },
        {
            "id": "3",
            "method": "get",
            "url": "localhost:8080/index.html",
            "header": {
                "content-type": "text/html; charset=utf-8"
            }
        }
    ]
    """;
    private static readonly string JsonSingleFakeRequestsCase = """
        {
            "id": "1",
            "method": "post",
            "header": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache"
            },
            "body": {
                "status": "Ok",
                "cool": 13
            }
        }
    """;

    [Test]
    public void Json_SingleRequest_ShouldConvert()
    {
        var fakeReqs = JsonConvert.DeserializeObject<FakeRequest>(JsonSingleFakeRequestsCase);
        Assert.That(fakeReqs, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(fakeReqs.Id, Is.EqualTo("1"));
            Assert.That(fakeReqs.Method, Is.EqualTo("post"));
            Assert.That(fakeReqs.Header, Has.Count.EqualTo(2));
            Assert.That(fakeReqs.Body, Is.Not.Null);
        });
        var firstHeader = fakeReqs.Header;
        Assert.Multiple(() =>
        {
            Assert.That(firstHeader, Is.Not.Null);
            if (firstHeader != null)
            {
                Assert.That(firstHeader.TryGetValue("content-type", out string? cT), Is.True);
                Assert.That(cT, Is.EqualTo("application/json"));
                Assert.That(firstHeader.TryGetValue("cache-control", out string? cC), Is.True);
                Assert.That(cC, Is.EqualTo("no-cache"));
            }
        });
    }

    [Test]
    public void Json_MultipleRequests_ShouldConvert()
    {
        var fakeReqs = JsonConvert.DeserializeObject<List<FakeRequest>>(JsonMultiFakeRequestsCase);
        Assert.That(fakeReqs, Has.Count.EqualTo(3));
        var secondReq = fakeReqs[1];
        Assert.Multiple(() =>
        {
            Assert.That(secondReq.Id, Is.EqualTo("2"));
            Assert.That(secondReq.Method, Is.EqualTo("post"));
            Assert.That(secondReq.Header, Has.Count.EqualTo(2));
            Assert.That(secondReq.Body, Is.Not.Null);
        });
        var thirdReq = fakeReqs[2];
        Assert.Multiple(() =>
        {
            Assert.That(thirdReq.Id, Is.EqualTo("3"));
            Assert.That(thirdReq.Method, Is.EqualTo("get"));
            Assert.That(thirdReq.Header, Has.Count.EqualTo(1));
            Assert.That(thirdReq.Body, Is.Null);
        });
        var thirdHeader = thirdReq.Header;
        Assert.Multiple(() =>
        {
            Assert.That(thirdHeader, Is.Not.Null);
            if (thirdHeader != null)
            {
                Assert.That(thirdHeader.TryGetValue("content-type", out string? cT), Is.True);
                Assert.That(cT, Is.EqualTo("text/html; charset=utf-8"));
            }
        });
    }
}
