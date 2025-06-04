class YamlStreamConverter :
    WriteOnlyJsonConverter<YamlStream>
{
    public override void Write(VerifyJsonWriter writer, YamlStream value)
    {
        writer.WriteStartArray();
        foreach (var child in value.Documents)
        {
            writer.Serialize(child);
        }
        writer.WriteEndArray();
    }
}