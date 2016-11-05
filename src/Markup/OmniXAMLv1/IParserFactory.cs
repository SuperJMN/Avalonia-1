namespace OmniXamlV1
{
    using ObjectAssembler;

    public interface IParserFactory
    {
        IParser Create(Settings settings);
    }
}