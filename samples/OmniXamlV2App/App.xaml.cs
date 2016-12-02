namespace AvaloniaApp
{
    using System;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Diagnostics;
    using Avalonia.DotNetFrameworkRuntime;
    using Avalonia.Logging.Serilog;
    using OmniXaml.Avalonia;
    using Serilog;
    using Serilog.Events;
    using ViewModels;

    class App : Application
    {
        public override void Initialize()
        {
            XamlService.Current.Load(this);
        }

        static void Main()
        {
            UriParser.Register(new ResourceManagerUriParser(), "resm", 0);

            InitializeLogging();
            AppBuilder.Configure<App>()
                .UseWin32()
                .UseDirect2D1()
                .SetupWithoutStarting();


            var window = new MyCustomWindow();
            window.DataContext = new MainWindowViewModel();

            window.Show();
            AttachDevTools(window);

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
                .WriteTo.LiterateConsole()
                .WriteTo.RollingFile("log-{Date}.txt")
                .WriteTo.Trace(outputTemplate: "{Area}: {Message}")
                .CreateLogger());
#endif
        }
    }
}
