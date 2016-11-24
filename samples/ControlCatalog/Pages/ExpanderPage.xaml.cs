using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ControlCatalog.Pages
{
    using OmniXaml.Avalonia;

    public class ExpanderPage : UserControl
    {
        public ExpanderPage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            XamlService.Current.Load(this);
        }
    }
}
