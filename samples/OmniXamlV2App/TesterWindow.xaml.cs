namespace AvaloniaApp
{
    using Avalonia.Controls;
    using OmniXaml.Avalonia;

    public class TesterWindow : Window
    {
        public TesterWindow()
        {
            XamlService.Current.Load(this);
        }
    }
}