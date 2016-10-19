﻿namespace AvaloniaApp
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

    class App : Application
    {

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        static void Main(string[] args)
        {
            InitializeLogging();
            AppBuilder.Configure<App>()
                .UseWin32()
                .UseDirect2D1()
                .SetupWithoutStarting();

            var window = (Window)new XamlLoader().Load(File.ReadAllText("Sample.xml"));

            var grid = (Grid)window.Content;
            var listBox = grid.Children.OfType<ListBox>().First();
            listBox.Items = new[] { "hola", "tío", "cómo estás?" };
            listBox.DataContext = new MainViewModel();

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

    internal class MainViewModel
    {
        public MainViewModel()
        {
            Title = "Título";
        }
        public string Title { get; set; }
    }
}
