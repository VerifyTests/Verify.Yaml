class YamlDocumentConverter :
    WriteOnlyJsonConverter<YamlDocument>
{
    public override void Write(VerifyJsonWriter writer, YamlDocument value) =>
        writer.Serialize(value.RootNode);
}