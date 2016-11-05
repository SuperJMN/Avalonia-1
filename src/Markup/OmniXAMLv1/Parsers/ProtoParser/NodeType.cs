namespace OmniXamlV1.Parsers.ProtoParser
{
    public enum NodeType
    {
        None,
        Element,
        EmptyElement,
        EndTag,
        PrefixDefinition,
        Directive,
        Attribute,
        Text,
        EmptyPropertyElement,
        PropertyElement
    }
}