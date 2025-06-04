[TestFixture]
public class Tests
{
    string yaml =
        """
        node:
          original: http://www.foo.com/
          short: foo
          error:
            code: 0
            msg: No action taken
        """;

    YamlDocument document;
    YamlStream stream;
    YamlMappingNode mappingNode;

    public Tests()
    {
        var input = new StringReader(yaml);
        stream = new();
        stream.Load(input);
        document = stream.Documents[0];
        mappingNode = (YamlMappingNode) document.RootNode;
    }

    [Test]
    public Task Document() =>
        Verify(document);

    [Test]
    public Task ScrubMember() =>
        Verify(document)
            .ScrubMember("short");

    [Test]
    public Task IgnoreMember() =>
        Verify(document)
            .IgnoreMember("short");

    [Test]
    public Task YamlStream() =>
        Verify(stream);

    [Test]
    public Task YamlNode()
    {
        var pair = mappingNode.Children[0];
        var node = pair.Value;
        return Verify(node);
    }

    [Test]
    public Task YamlMappingNode() =>
        Verify(mappingNode);
}