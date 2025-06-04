class YamlMappingNodeConverter :
    WriteOnlyJsonConverter<YamlMappingNode>
{
    public override void Write(VerifyJsonWriter writer, YamlMappingNode node)
    {
        writer.WriteStartObject();

        foreach (var (key, value) in node.Reverse())
        {
            var name = key.ToString();
            writer.WriteMember(node, value, name);
        }

        writer.WriteEndObject();
    }
}