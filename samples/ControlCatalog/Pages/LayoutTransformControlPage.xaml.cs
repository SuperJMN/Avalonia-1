using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ControlCatalog.Pages
{
    using OmniXaml.Avalonia;

    public class LayoutTransformControlPage : UserControl
    {
        public LayoutTransformControlPage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            XamlService.Current.Load(this);
        }
    }
}
