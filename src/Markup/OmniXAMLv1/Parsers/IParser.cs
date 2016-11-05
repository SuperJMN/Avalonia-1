namespace OmniXamlV1.Parsers
{
    public interface IParser<in TInput, out TOutput>
    {
        TOutput Parse(TInput input);
    }
}