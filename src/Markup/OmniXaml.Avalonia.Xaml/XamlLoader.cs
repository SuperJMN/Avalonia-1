namespace OmniXaml.Avalonia
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ambient;
    using Context;
    using Data;
    using global::Avalonia;
    using global::Avalonia.Animation;
    using global::Avalonia.Controls;
    using global::Avalonia.Input;
    using global::Avalonia.Markup;
    using global::Avalonia.Media;
    using global::Avalonia.Platform;
    using global::Avalonia.Styling;
    using Services;
    using Styling;
    using Templates;
    using TypeLocation;

    public class XamlLoader : IXamlLoader
    {
        private readonly ObjectBuilderContext contructionContext;
        private readonly ITypeDirectory directory;
        private readonly MetadataProvider metadataProvider;

        public XamlLoader()
        {
            metadataProvider = new MetadataProvider();
            directory = GetCoolDirectory();

            contructionContext = new ObjectBuilderContext(
                Registrator.GetSourceValueConverter(),
                metadataProvider);
        }

        private ITypeDirectory GetCoolDirectory()
        {
            IEnumerable<Assembly> ForcedAssemblies = new[]
            {
                typeof(AvaloniaObject).GetTypeInfo().Assembly,
                typeof(Control).GetTypeInfo().Assembly,
                typeof(Style).GetTypeInfo().Assembly,
                typeof(DataTemplate).GetTypeInfo().Assembly,
                typeof(SolidColorBrush).GetTypeInfo().Assembly,
                typeof(IValueConverter).GetTypeInfo().Assembly,
                typeof(StyleInclude).GetTypeInfo().Assembly,
            };

            var loadedAssemblies = AvaloniaLocator.Current
                .GetService<IRuntimePlatform>()
                ?.GetLoadedAssemblies() ?? new Assembly[0];

            var scanned = loadedAssemblies.Except(ForcedAssemblies);

            var assemblies = ForcedAssemblies.Concat(scanned);
            return new AttributeBasedTypeDirectory(assemblies.ToList());
        }

        public ConstructionResult Load(string xaml)
        {
            var objectBuilder = new AvaloniaObjectBuilder(
                new InstanceCreator(contructionContext.SourceValueConverter, contructionContext, directory),
                contructionContext,
                new ContextFactory(directory, contructionContext));

            var cons = GetConstructionNode(xaml);

            var namescopeAnnotator = new NamescopeAnnotator(contructionContext.MetadataProvider);

            var trackingContext = new BuildContext(namescopeAnnotator, new AmbientRegistrator(), new AvaloniaLifeCycleSignaler())
            {
                PrefixAnnotator = cons.PrefixAnnotator,
                PrefixedTypeResolver = new PrefixedTypeResolver(cons.PrefixAnnotator, directory)
            };

            trackingContext.Bag.Add("Uri", @"file:\\\");

            return new ConstructionResult(objectBuilder.Inflate(cons.Root, trackingContext), namescopeAnnotator);
        }

        public ConstructionResult Load(string xaml, object rootInstance)
        {
            var objectBuilder = new AvaloniaObjectBuilder(
                new InstanceCreator(contructionContext.SourceValueConverter, contructionContext, directory),
                contructionContext,
                new ContextFactory(directory, contructionContext));

            var cons = GetConstructionNode(xaml);

            var namescopeAnnotator = new NamescopeAnnotator(contructionContext.MetadataProvider);

            var trackingContext = new BuildContext(namescopeAnnotator, new AmbientRegistrator(), new AvaloniaLifeCycleSignaler())
            {
                PrefixAnnotator = cons.PrefixAnnotator,
                PrefixedTypeResolver = new PrefixedTypeResolver(cons.PrefixAnnotator, directory)
            };

            var inflatedInstance = objectBuilder.Inflate(cons.Root, trackingContext, rootInstance);
            return new ConstructionResult(inflatedInstance, namescopeAnnotator);
        }

        private TypeDirectory GetTypeDirectory()
        {
            var rootNs = XamlNamespace
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
                            typeof(MultiBinding).GetTypeInfo().Namespace, "OmniXaml.Avalonia.Data", "OmniXaml.Avalonia.MarkupExtensions", typeof(StyleInclude).Namespace),

                    //Route.Assembly(typeof(HtmlLabel).GetTypeInfo().Assembly)
                    //    .WithNamespaces(typeof(HtmlLabel).GetTypeInfo().Namespace),

                    Route.Assembly(typeof(BoolConverters).GetTypeInfo().Assembly)
                        .WithNamespaces(typeof(BoolConverters).GetTypeInfo().Namespace),

                    Route.Assembly(typeof(Styles).GetTypeInfo().Assembly)
                        .WithNamespaces(typeof(Styles).Namespace)
                );

            var mutableNs = XamlNamespace.Map("https://github.com/avaloniaui/mutable")
                .With(
                    Route.Assembly(typeof(global::Avalonia.Media.Mutable.SolidColorBrush).GetTypeInfo().Assembly)
                        .WithNamespaces(typeof(global::Avalonia.Media.Mutable.SolidColorBrush).Namespace));



            return new TypeDirectory(new[] {rootNs, mutableNs});
        }

        private ParseResult GetConstructionNode(string xaml)
        {
            var resolver = new Resolver(directory);
            var sut = new XamlToTreeParser(metadataProvider, new[] {new InlineParser(resolver) }, resolver);
            var tree = sut.Parse(xaml, new PrefixAnnotator());
            return tree;
        }
    }
}