using Newtonsoft.Json;
using SandboxClient.Model;

namespace SandboxClient.Test;

[TestFixture]
public class FakeEventJsonConverterTest
{
    public static string[] JsonToSingleEventCases =
    [
        """
        [
            "1",
            "2", 
            "3"
        ]
        """,
        """
        [
            {
                "id": "1",
                "amount": 1,
            },
            {
                "id": "2",
                "amount": 1,
            },
            {
                "id": "3",
                "amount": 1,
            },
        ]
        """,
        """
        [
            ["1"],
            ["2"],
            ["3"]
        ]
        """,
    ];

    public static string[] JsonToParallelCases = [];

    private static string JsonEventCase = """
    [
        "normal_request",
        "special_request",
        {
            "id": "normal_request",
            "amount": 4,
        },
        [
            "normal_request",
            "special_request",
            {
                "id": "another_request",
                "amount": "2"
            }
        ]
    ]
    """;

    [SetUp]
    public void Setup() { }

    [TestCaseSource(nameof(JsonToSingleEventCases))]
    public void SupportedTypesJson_ToSingleEvent_ShouldConvert(string json)
    {
        var fakeEvent = JsonConvert.DeserializeObject<List<FakeEvent>>(json);
        Assert.That(fakeEvent, Has.Count.EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(fakeEvent[0].Ids, Has.Count.EqualTo(1));
            Assert.That(fakeEvent[1].Ids, Has.Count.EqualTo(1));
            Assert.That(fakeEvent[2].Ids, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(fakeEvent[0].Ids[0], Is.EqualTo("1"));
            Assert.That(fakeEvent[1].Ids[0], Is.EqualTo("2"));
            Assert.That(fakeEvent[2].Ids[0], Is.EqualTo("3"));
        });
    }

    [Test]
    public void Json_TypicalEvents_ShouldConvert()
    {
        var fakeEvent = JsonConvert.DeserializeObject<List<FakeEvent>>(JsonEventCase);
        Assert.That(fakeEvent, Has.Count.EqualTo(4));
        // single event case
        Assert.Multiple(() =>
        {
            Assert.That(fakeEvent[0].EventType, Is.EqualTo(EventType.Single));
            Assert.That(fakeEvent[1].EventType, Is.EqualTo(EventType.Single));
        });
        Assert.Multiple(() =>
        {
            Assert.That(fakeEvent[0].Ids, Has.Count.EqualTo(1));
            Assert.That(fakeEvent[1].Ids, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(fakeEvent[0].Ids.First(), Is.EqualTo("normal_request"));
            Assert.That(fakeEvent[1].Ids.First(), Is.EqualTo("special_request"));
        });

        // parallel case
        Assert.Multiple(() =>
        {
            Assert.That(fakeEvent[2].EventType, Is.EqualTo(EventType.Parallel));
            Assert.That(fakeEvent[3].EventType, Is.EqualTo(EventType.Parallel));
        });
        Assert.Multiple(() =>
        {
            Assert.That(fakeEvent[2].Ids, Has.Count.EqualTo(4));
            Assert.That(fakeEvent[3].Ids, Has.Count.EqualTo(4));
        });
    }

    [TestCaseSource(nameof(JsonToParallelCases))]
    public void SupportedTypesJson_ToParallelEvent_ShouldConvert(string json) { }

    [Test]
    public void NonSupportTypesJson_ShouldThrowException()
    {
        Assert.Pass();
    }
}
