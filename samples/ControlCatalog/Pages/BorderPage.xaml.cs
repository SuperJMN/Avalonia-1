using Avalonia.Controls;

namespace ControlCatalog.Pages
{
    using OmniXaml.Avalonia;

    public class BorderPage : UserControl
    {
        public BorderPage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            XamlService.Current.Load(this);
        }
    }
}
