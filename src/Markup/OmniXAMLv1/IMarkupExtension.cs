namespace OmniXamlV1
{
    public interface IMarkupExtension
    {
        object ProvideValue(MarkupExtensionContext markupExtensionContext);
    }
}