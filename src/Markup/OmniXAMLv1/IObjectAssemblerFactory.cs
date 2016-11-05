namespace OmniXamlV1
{
    using ObjectAssembler;

    public interface IObjectAssemblerFactory
    {
        IObjectAssembler CreateAssembler(Settings settings);        
    }
}