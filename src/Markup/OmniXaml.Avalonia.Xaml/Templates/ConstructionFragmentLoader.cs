namespace OmniXaml.Avalonia.Templates
{
    using Metadata;
    using OmniXaml;

    public class ConstructionFragmentLoader : IConstructionFragmentLoader
    {
        public object Load(ConstructionNode node, IObjectBuilder builder, TrackingContext trackingContext)
        {
            return new TemplateContent(node, builder, trackingContext);
        }
    }
}