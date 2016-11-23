namespace AvaloniaApp
{
    using Avalonia.Controls;
    using Avalonia.Input;
    using OmniXaml.Avalonia;

    public class MyCustomWindow : Window
    {
        public MyCustomWindow()
        {
            XamlService.Current.Load(this);
        }

        private void OnPointerPressed(object sender, PointerPressedEventArgs pointerPressedEventArgs)
        {
        }
    }
}