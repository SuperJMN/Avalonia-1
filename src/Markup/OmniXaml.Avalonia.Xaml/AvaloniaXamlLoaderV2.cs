namespace OmniXaml.Avalonia
{
    using System.Reflection;
    using Ambient;
    using Context;
    using Data;
    using global::Avalonia;
    using global::Avalonia.Animation;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Html;
    using global::Avalonia.Input;
    using global::Avalonia.Markup;
    using global::Avalonia.Media;
    using global::Avalonia.Styling;
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
            return new ConstructionResult(objectBuilder.Inflate(cons, trackingContext), namescopeAnnotator);
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
            var instance = objectBuilder.Inflate(cons, intance, trackingContext);
            return new ConstructionResult(instance, namescopeAnnotator);
        }

        private TypeDirectory GetTypeDirectory()
        {
            var typeDirectory = new TypeDirectory();

            typeDirectory.AddNamespace(
                XamlNamespace
                    .Map("https://github.com/avaloniaui")
                    .With(
                        Route
                            .Assembly(typeof(Window).GetTypeInfo().Assembly)
                            .WithNamespaces(
                                "Avalonia.Controls",
                                typeof(Application).Namespace,
                                "Avalonia.Controls.Presenters",
                                "Avalonia.Controls.Shapes",
                                "Avalonia.Controls.Primitives",
                                "Avalonia.Controls.Embedding"),

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
                            .WithNamespaces(typeof(DataTemplate).Namespace),

                        Route.Assembly(typeof(MultiBinding).GetTypeInfo().Assembly)
                            .WithNamespaces(
                                typeof(MultiBinding).GetTypeInfo().Namespace, "OmniXaml.Avalonia.Data", "OmniXaml.Avalonia.MarkupExtensions", typeof(StyleInclude).Namespace)
                                ,

                        Route.Assembly(typeof(HtmlLabel).GetTypeInfo().Assembly)
                            .WithNamespaces(typeof(HtmlLabel).GetTypeInfo().Namespace),

                        Route.Assembly(typeof(BoolConverters).GetTypeInfo().Assembly)
                            .WithNamespaces(typeof(BoolConverters).GetTypeInfo().Namespace),

                        Route.Assembly(typeof(Styles).GetTypeInfo().Assembly)
                            .WithNamespaces(typeof(Styles).Namespace)
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