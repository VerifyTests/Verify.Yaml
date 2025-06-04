namespace VerifyTests;

public static class VerifyYaml
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings
            .AddExtraSettings(_ =>
            {
                var converters = _.Converters;
                converters.Add(new YamlStreamConverter());
                converters.Add(new YamlDocumentConverter());
                converters.Add(new YamlMappingNodeConverter());
                converters.Add(new YamlSequenceNodeConverter());
                converters.Add(new YamlScalarNodeConverter());
            });
    }
}