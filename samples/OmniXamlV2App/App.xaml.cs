namespace AvaloniaApp
{
    using Avalonia;
    using Avalonia.Logging.Serilog;
    using OmniXaml.Avalonia;
    using Serilog;
    using ViewModels;

    class App : Application
    {
        public override void Initialize()
        {
            XamlService.Current = new XamlService();
            XamlService.Current.Load(this);
        }

        static void Main()
        {
            InitializeLogging();
            AppBuilder.Configure<App>()
                .UseWin32()
                .UseDirect2D1()
                .SetupWithoutStarting();

            
            var window = new MyCustomWindow();
            window.DataContext = new MainWindowViewModel();

            window.Show();
            //AttachDevTools(window);

            Current.Run(window);
        }

        //        public static void AttachDevTools(Window window)
        //        {
        //#if DEBUG
        //            DevTools.Attach(window);
        //#endif
        //        }

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
