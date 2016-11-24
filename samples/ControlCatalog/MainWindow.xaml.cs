using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ControlCatalog
{
    using OmniXaml.Avalonia;

    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            //this.AttachDevTools();
        }

        private void InitializeComponent()
        {
            // TODO: iOS does not support dynamically loading assemblies
            // so we must refer to this resource DLL statically. For
            // now I am doing that here. But we need a better solution!!
            //var theme = new DefaultTheme();
            //theme.FindResource("Button");
            XamlService.Current.Load(this);
        }
    }
}
