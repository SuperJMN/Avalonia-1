namespace OmniXamlV1
{
    public interface INameScope
    {
        void Register(string name, object scopedElement);
        object Find(string name);
        void Unregister(string name);
    } 
}