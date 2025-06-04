class YamlDocumentConverter :
    WriteOnlyJsonConverter<YamlDocument>
{
    public override void Write(VerifyJsonWriter writer, YamlDocument value) =>
        writer.Serialize(ToObject(value.RootNode)!);

    static object? ToObject(YamlNode node)
    {
        switch (node)
        {
            case YamlMappingNode mapping:
                return mapping.Children.ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => ToObject(kvp.Value));
            case YamlSequenceNode sequence:
                return sequence.Children.Select(ToObject).ToList();
            case YamlScalarNode scalar:
                return scalar.Value;
            default:
                return null;
        }
    }
}