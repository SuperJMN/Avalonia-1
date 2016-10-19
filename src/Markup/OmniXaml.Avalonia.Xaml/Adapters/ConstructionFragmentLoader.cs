namespace OmniXaml.Avalonia.Adapters
{
    using Metadata;
    using OmniXaml;

    public class ConstructionFragmentLoader : IConstructionFragmentLoader
    {
        public object Load(ConstructionNode node, IObjectBuilder builder)
        {
            return new TemplateContent(node, builder);
        }
    }
}