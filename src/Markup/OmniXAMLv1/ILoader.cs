namespace OmniXamlV1
{
    using System.IO;
    using ObjectAssembler;

    public interface ILoader
    {
        object Load(Stream stream, Settings settings);
    }
}