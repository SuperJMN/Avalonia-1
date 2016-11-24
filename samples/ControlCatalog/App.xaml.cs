using Avalonia;

namespace ControlCatalog
{
    using OmniXaml.Avalonia;

    public class App : Application
    {
        public override void Initialize()
        {
            XamlService.Current.Load(this);
        }
    }
}
