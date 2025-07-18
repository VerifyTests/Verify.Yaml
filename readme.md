# <img src="/src/icon.png" height="30px"> Verify.Yaml

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/2w0tvfpv56txfale?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-Yaml)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Yaml.svg)](https://www.nuget.org/packages/Verify.Yaml/)

Adds [Verify](https://github.com/VerifyTests/Verify) support for converting [YamlDotNet](https://github.com/aaubry/YamlDotNet) types.<!-- singleLineInclude: intro. path: /docs/intro.include.md -->

**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors


### Entity Framework Extensions<!-- include: zzz. path: /docs/zzz.include.md -->

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.Yaml) is a major sponsor and is proud to contribute to the development this project.

[![Entity Framework Extensions](https://raw.githubusercontent.com/VerifyTests/Verify.Yaml/refs/heads/main/docs/zzz.png)](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.Yaml)<!-- endInclude -->


## NuGet

 * https://nuget.org/packages/Verify.Yaml


## Usage

<!-- snippet: enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Init() =>
    VerifyYaml.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### YamlDocument

<!-- snippet: YamlDocumentSample -->
<a id='snippet-YamlDocumentSample'></a>
```cs
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
```
<sup><a href='/src/Tests/Samples.cs#L4-L25' title='Snippet source file'>snippet source</a> | <a href='#snippet-YamlDocumentSample' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.YamlDocumentSample.verified.txt -->
<a id='snippet-Samples.YamlDocumentSample.verified.txt'></a>
```txt
[
  {
    node: {
      error: {
        msg: No action taken,
        code: 0
      },
      short: foo,
      original: http://www.foo.com/
    }
  }
]
```
<sup><a href='/src/Tests/Samples.YamlDocumentSample.verified.txt#L1-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.YamlDocumentSample.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Ignoring and Scrubbing

Values in the json can be ignored or scrubbed:

<!-- snippet: ScrubIgnoreMember -->
<a id='snippet-ScrubIgnoreMember'></a>
```cs
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
```
<sup><a href='/src/Tests/Samples.cs#L27-L50' title='Snippet source file'>snippet source</a> | <a href='#snippet-ScrubIgnoreMember' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.ScrubIgnoreMemberSample.verified.txt -->
<a id='snippet-Samples.ScrubIgnoreMemberSample.verified.txt'></a>
```txt
[
  {
    node: {
      error: {
        code: 0
      },
      short: Scrubbed,
      original: http://www.foo.com/
    }
  }
]
```
<sup><a href='/src/Tests/Samples.ScrubIgnoreMemberSample.verified.txt#L1-L11' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.ScrubIgnoreMemberSample.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Dates and Guid scrubbing

Json values that map to known date and time formats are scrubbed. See [Guids scrubbing](https://github.com/VerifyTests/Verify/blob/main/docs/guids.md) and [Date scrubbing](https://github.com/VerifyTests/Verify/blob/main/docs/dates.md)

<!-- snippet: GuidsAndDates -->
<a id='snippet-GuidsAndDates'></a>
```cs
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
```
<sup><a href='/src/Tests/Samples.cs#L52-L73' title='Snippet source file'>snippet source</a> | <a href='#snippet-GuidsAndDates' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.GuidsAndDatesSample.verified.txt -->
<a id='snippet-Samples.GuidsAndDatesSample.verified.txt'></a>
```txt
[
  {
    node: {
      error: {
        msg: No action taken,
        guid: Guid_1
      },
      short: foo,
      date: Date_1
    }
  }
]
```
<sup><a href='/src/Tests/Samples.GuidsAndDatesSample.verified.txt#L1-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.GuidsAndDatesSample.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Inline dates and Guids

Inline dates and Guids can be scrubbed:

<!-- snippet: InlineGuidsAndDates -->
<a id='snippet-InlineGuidsAndDates'></a>
```cs
[Test]
public Task InlineGuidsAndDatesSample()
{
    var yaml =
        """
        node:
          date: 2023/10/01
          short: foo 2023/10/01
          error:
            guid: 123e4567-e89b-12d3-a456-426614174000
            msg: No action taken 123e4567-e89b-12d3-a456-426614174000
        """;

    var input = new StringReader(yaml);
    var yamlStream = new YamlStream();
    yamlStream.Load(input);
    return Verify(yamlStream)
        .ScrubInlineDates("yyyy/MM/dd")
        .ScrubInlineGuids();
}
```
<sup><a href='/src/Tests/Samples.cs#L75-L99' title='Snippet source file'>snippet source</a> | <a href='#snippet-InlineGuidsAndDates' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.InlineGuidsAndDatesSample.verified.txt -->
<a id='snippet-Samples.InlineGuidsAndDatesSample.verified.txt'></a>
```txt
[
  {
    node: {
      error: {
        msg: No action taken Guid_1,
        guid: Guid_1
      },
      short: foo Date_1,
      date: Date_1
    }
  }
]
```
<sup><a href='/src/Tests/Samples.InlineGuidsAndDatesSample.verified.txt#L1-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.InlineGuidsAndDatesSample.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Inline date and Guids scrubbing can also be defined globally:

  * [VerifierSettings.ScrubInlineDateTimes](https://github.com/VerifyTests/Verify/blob/main/docs/dates.md#globally-2)
  * [VerifierSettings.ScrubInlineGuids](https://github.com/VerifyTests/Verify/blob/main/docs/guids.md#globally-1)


## Icon

[Pattern](https://thenounproject.com/icon/yaml-file-extension-3015661/) designed by [Grafix Point](https://thenounproject.com/creator/virtualdesign/) from [The Noun Project](https://thenounproject.com/).
