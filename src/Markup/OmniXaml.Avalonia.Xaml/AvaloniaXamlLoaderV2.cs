namespace OmniXaml.Avalonia
{
    using System.Reflection;
    using Ambient;
    using Context;
    using global::Avalonia;
    using global::Avalonia.Animation;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Html;
    using global::Avalonia.Input;
    using global::Avalonia.Media;
    using global::Avalonia.Styling;
    using MarkupExtensions;
    using Styling;
    using Templates;
    using Tests;
    using TypeLocation;

    public class AvaloniaXamlLoaderV2 : IXamlLoader
    {
        private readonly ObjectBuilderContext contructionContext;
        private readonly TypeDirectory directory;
        private readonly MetadataProvider metadataProvider;

        public AvaloniaXamlLoaderV2()
        {
            metadataProvider = new MetadataProvider();
            directory = GetTypeDirectory();

            contructionContext = new ObjectBuilderContext(
                Registrator.GetSourceValueConverter(directory),
                metadataProvider);
        }

        public ConstructionResult Load(string xaml)
        {
            var objectBuilder = new AvaloniaObjectBuilder(
                new InstanceCreator(contructionContext.SourceValueConverter, contructionContext, directory),
                contructionContext,
                new ContextFactory(directory, contructionContext));
            var cons = GetConstructionNode(xaml);
            var namescopeAnnotator = new NamescopeAnnotator(contructionContext.MetadataProvider);
            var trackingContext = new BuildContext(namescopeAnnotator, new AmbientRegistrator(), new AvaloniaLifeCycleSignaler());
            trackingContext.Bag.Add("Uri", @"file:\\\");
            return new ConstructionResult(objectBuilder.Create(cons, trackingContext), namescopeAnnotator);
        }

        public ConstructionResult Load(string xaml, object intance)
        {
            var objectBuilder = new AvaloniaObjectBuilder(
                new InstanceCreator(contructionContext.SourceValueConverter, contructionContext, directory),
                contructionContext,
                new ContextFactory(directory, contructionContext));
            var cons = GetConstructionNode(xaml);
            var namescopeAnnotator = new NamescopeAnnotator(contructionContext.MetadataProvider);
            var trackingContext = new BuildContext(namescopeAnnotator, new AmbientRegistrator(), new AvaloniaLifeCycleSignaler());
            var instance = objectBuilder.Create(cons, intance, trackingContext);
            return new ConstructionResult(instance, namescopeAnnotator);
        }

        private TypeDirectory GetTypeDirectory()
        {
            var typeDirectory = new TypeDirectory();

            var type = typeof(Window);
            var ass = type.GetTypeInfo().Assembly;
            var htmlControl = typeof(HtmlLabel).GetTypeInfo();

            typeDirectory.AddNamespace(
                XamlNamespace
                    .Map("https://github.com/avaloniaui")
                    .With(
                        Route
                            .Assembly(ass)
                            .WithNamespaces("Avalonia.Controls", typeof(Application).Namespace, "Avalonia.Controls.Presenters", "Avalonia.Controls.Shapes", "Avalonia.Controls.Primitives", "Avalonia.Controls.Embedding"),
                        Route
                            .Assembly(typeof(Color).GetTypeInfo().Assembly)
                            .WithNamespaces(typeof(Color).Namespace),
                        Route
                            .Assembly(typeof(KeyboardNavigation).GetTypeInfo().Assembly)
                            .WithNamespaces(typeof(KeyboardNavigation).Namespace),
                        Route
                            .Assembly(typeof(CrossFade).GetTypeInfo().Assembly)
                            .WithNamespaces(typeof(CrossFade).Namespace),
                        Route
                            .Assembly(typeof(DataTemplate).GetTypeInfo().Assembly)
                            .WithNamespaces(
                                typeof(DataTemplate).Namespace,
                                typeof(BindingExtension).Namespace),
                        Route.Assembly(htmlControl.Assembly)
                            .WithNamespaces(htmlControl.Namespace),
                        Route.Assembly(typeof(StyleInclude).GetTypeInfo().Assembly)
                            .WithNamespaces(typeof(StyleInclude).Namespace),
                        Route.Assembly(typeof(Styles).GetTypeInfo().Assembly).WithNamespaces(typeof(Styles).Namespace)
                    )
                );

            typeDirectory.AddNamespace(
                XamlNamespace.Map("https://github.com/avaloniaui/mutable")
                    .With(
                        Route.Assembly(typeof(global::Avalonia.Media.Mutable.SolidColorBrush).GetTypeInfo().Assembly)
                            .WithNamespaces(typeof(global::Avalonia.Media.Mutable.SolidColorBrush).Namespace)));

            typeDirectory.RegisterPrefix(new PrefixRegistration(string.Empty, "https://github.com/avaloniaui"));

            return typeDirectory;
        }

        private ConstructionNode GetConstructionNode(string xaml)
        {
            var sut = new XamlToTreeParser(directory, metadataProvider, new[] {new InlineParser(directory)});
            var tree = sut.Parse(xaml);
            return tree;
        }
    }
}