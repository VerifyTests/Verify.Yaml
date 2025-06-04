class YamlSequenceNodeConverter :
    WriteOnlyJsonConverter<YamlSequenceNode>
{
    public override void Write(VerifyJsonWriter writer, YamlSequenceNode node)
    {
        writer.WriteStartArray();
        foreach (var child in node.Children.Reverse())
        {
            writer.Serialize(child);
        }
        writer.WriteEndArray();
    }
}