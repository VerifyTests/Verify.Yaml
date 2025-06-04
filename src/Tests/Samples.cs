using YamlDotNet.RepresentationModel;

[TestFixture]
public class Samples
{
    #region YamlDocumentSample

    [Test]
    public Task YamlDocumentSample()
    {
        var yaml =
            """
            node:
              original: http://www.foo.com/
              short: foo
              error:
                code: 0
                msg: No action taken
            """;

        var input = new StringReader(yaml);
        var yamlStream = new YamlStream();
        yamlStream.Load(input);
        return Verify(yamlStream);
    }

    #endregion
    #region ScrubIgnoreMember

    [Test]
    public Task ScrubIgnoreMemberSample()
    {
        var yaml =
            """
            node:
              original: http://www.foo.com/
              short: foo
              error:
                code: 0
                msg: No action taken
            """;

        var input = new StringReader(yaml);
        var yamlStream = new YamlStream();
        yamlStream.Load(input);
        return Verify(yamlStream)
            .ScrubMember("short")
            .IgnoreMember("msg");
    }

    #endregion
    #region GuidsAndDates

    [Test]
    public Task GuidsAndDatesSample()
    {
        var yaml =
            """
            node:
              date: 1/10/2023
              short: foo
              error:
                guid: 123e4567-e89b-12d3-a456-426614174000
                msg: No action taken
            """;

        var input = new StringReader(yaml);
        var yamlStream = new YamlStream();
        yamlStream.Load(input);
        return Verify(yamlStream);
    }

    #endregion
    #region InlineGuidsAndDates

    [Test]
    public Task InlineGuidsAndDatesSample()
    {
        var yaml =
            """
            node:
              date: 01/10/2023
              short: foo 01/10/2023
              error:
                guid: 123e4567-e89b-12d3-a456-426614174000
                msg: No action taken 123e4567-e89b-12d3-a456-426614174000
            """;


        var input = new StringReader(yaml);
        var yamlStream = new YamlStream();
        yamlStream.Load(input);
        return Verify(yamlStream)
            .ScrubInlineDates("dd/MM/yyyy")
            .ScrubInlineGuids();
    }

    #endregion
}