namespace AvaloniaApp
{
    using System.IO;
    using System.Linq;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Diagnostics;
    using Avalonia.Logging.Serilog;
    using Avalonia.Markup.Xaml;
    using OmniXaml.Avalonia.Context;
    using Serilog;
    using AvaloniaXamlLoader = OmniXaml.Avalonia.AvaloniaXamlLoader;

    class App : Application
    {

        public override void Initialize()
        {
            Avalonia.Markup.Xaml.AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        static void Main(string[] args)
        {
            InitializeLogging();
            AppBuilder.Configure<App>()
                .UseWin32()
                .UseDirect2D1()
                .SetupWithoutStarting();

            var window = (Window)new AvaloniaXamlLoader().Load(File.ReadAllText("Sample.xml"));
            window.DataContext = new MainViewModel();

            //var grid = (Grid)window.Content;
            //var listBox = grid.Children.OfType<ListBox>().First();
            //listBox.Items = new[] { "hola", "tío", "cómo estás?" };
            //listBox.DataContext = new MainViewModel();

            window.Show();

            Current.Run(window);
        }

        public static void AttachDevTools(Window window)
        {
#if DEBUG
            DevTools.Attach(window);
#endif
        }

        private static void InitializeLogging()
        {
#if DEBUG
            SerilogLogger.Initialize(new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Trace(outputTemplate: "{Area}: {Message}")
                .CreateLogger());
#endif
        }
    }
}
