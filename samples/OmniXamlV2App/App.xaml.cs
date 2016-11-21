namespace AvaloniaApp
{
    using System;
    using System.IO;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Diagnostics;
    using Avalonia.Logging.Serilog;
    using Avalonia.Platform;
    using OmniXaml.Avalonia;
    using Serilog;
    using ViewModels;

    class App : Application
    {
        private static readonly AvaloniaXamlLoaderV2 Loader = new AvaloniaXamlLoaderV2();

        public override void Initialize()
        {
            var assetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>();

            using (var stream = new StreamReader(assetLoader.Open(new Uri("resm:AvaloniaApp.App.xaml?assembly=AvaloniaApp"))))
            {
                var xaml = stream.ReadToEnd();
                Loader.Load(xaml, this);
            }
        }

        static void Main()
        {
            InitializeLogging();
            AppBuilder.Configure<App>()
                .UseWin32()
                .UseDirect2D1()
                .SetupWithoutStarting();

            var window = (Window)Loader.Load(File.ReadAllText("MyCustomWindow.xaml")).Instance;
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
