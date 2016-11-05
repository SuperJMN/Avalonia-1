namespace OmniXamlV1.Typing
{
    public class XamlTypeName
    {
        public XamlTypeName(string xamlNamespace, string name)
        {
            Name = name;
            Namespace = xamlNamespace;
        }

        public string Name
        {
            get; private set;
        }

        public string Namespace
        {
            get; private set;
        }
    }
}