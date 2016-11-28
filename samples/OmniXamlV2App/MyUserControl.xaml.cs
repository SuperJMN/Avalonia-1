namespace AvaloniaApp
{
    using Avalonia.Controls;
    using OmniXaml.Avalonia;

    public class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            XamlService.Current.Load(this);
        }
    }
}