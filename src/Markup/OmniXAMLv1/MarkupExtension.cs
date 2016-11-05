namespace OmniXamlV1
{
    public abstract class MarkupExtension : IMarkupExtension
    {
        public abstract object ProvideValue(MarkupExtensionContext markupExtensionContext);        
    }
}