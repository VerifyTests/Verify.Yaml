using YamlDotNet.RepresentationModel;

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
    YamlStream yamlStream;

    public Tests()
    {
        var input = new StringReader(yaml);
        yamlStream = new();
        yamlStream.Load(input);
        document = yamlStream.Documents[0];
    }

    [Test]
    public Task Document() =>
        Verify(document);
}