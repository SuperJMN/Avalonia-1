namespace OmniXamlV1.ObjectAssembler
{
    using System.Collections.Generic;

    public class Settings
    {
        public object RootInstance { get; set; }
        public IInstanceLifeCycleListener InstanceLifeCycleListener { get; set; }
        public IReadOnlyDictionary<string, object> ParsingContext { get; set; }
    }
}