using Avalonia;

namespace ControlCatalog
{
    public class App : Application
    {
        public override void Initialize()
        {
            new XamlLoader().Load(this);
        }
    }
}
