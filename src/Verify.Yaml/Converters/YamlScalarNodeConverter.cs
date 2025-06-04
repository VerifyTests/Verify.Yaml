class YamlScalarNodeConverter :
    WriteOnlyJsonConverter<YamlScalarNode>
{
    public override void Write(VerifyJsonWriter writer, YamlScalarNode node)
    {
        if (node.Value == null)
        {
            writer.WriteNull();
        }
        else
        {
            writer.Serialize(node.Value);
        }
    }
}