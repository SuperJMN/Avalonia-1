namespace OmniXaml.Avalonia
{
    using System;
    using System.Reflection;
    using Ambient;
    using Context;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Html;
    using Templates;
    using TypeLocation;

    public class AvaloniaXamlLoader : IXamlLoader
    {
        private readonly TypeDirectory directory;
        private readonly MetadataProvider metadataProvider;
        private readonly ConstructionContext contructionContext;

        public AvaloniaXamlLoader()
        {
            metadataProvider = new MetadataProvider();
            directory = GetTypeDirectory();

            contructionContext = new ConstructionContext(
               new InstanceCreator(),
               Registrator.GetSourceValueConverter(),
               metadataProvider);
        }

        private TypeDirectory GetTypeDirectory()
        {
            var typeDirectory = new TypeDirectory();

            var type = typeof(Window);
            var ass = type.GetTypeInfo().Assembly;
            var htmlControl = typeof(HtmlLabel).GetTypeInfo();

            typeDirectory.AddNamespace(
                XamlNamespace
                    .Map("root")
                    .With(
                        Route
                            .Assembly(ass)
                            .WithNamespaces("Avalonia.Controls"),
                        Route
                            .Assembly(typeof(DataTemplate).GetTypeInfo().Assembly)
                            .WithNamespaces(
                                typeof(DataTemplate).Namespace,
                                typeof(MarkupExtensions.BindingExtension).Namespace),
                        Route.Assembly(htmlControl.Assembly)
                            .WithNamespaces(htmlControl.Namespace)));


            typeDirectory.RegisterPrefix(new PrefixRegistration(string.Empty, "root"));

            return typeDirectory;
        }

        public ConstructionResult Load(string xaml)
        {           
            var objectBuilder = new AvaloniaObjectBuilder(contructionContext, (assignment, context, tc) => new MarkupExtensionContext(assignment, contructionContext, directory, tc));
            var cons = GetConstructionNode(xaml);
            var namescopeAnnotator = new NamescopeAnnotator(contructionContext.MetadataProvider);
            var trackingContext = new TrackingContext(namescopeAnnotator, new AmbientRegistrator(), new AvaloniaLifeCycleSignaler() );
            return new ConstructionResult(objectBuilder.Create(cons, trackingContext), namescopeAnnotator);
        }

        public ConstructionResult Load(string xaml, object intance)
        {
            var objectBuilder = new AvaloniaObjectBuilder(contructionContext, (assignment, context, tc) => new MarkupExtensionContext(assignment, contructionContext, directory, tc));
            var cons = GetConstructionNode(xaml);
            var namescopeAnnotator = new NamescopeAnnotator(contructionContext.MetadataProvider);
            var trackingContext = new TrackingContext(namescopeAnnotator, new AmbientRegistrator(), new AvaloniaLifeCycleSignaler());
            return new ConstructionResult(objectBuilder.Create(cons, intance, trackingContext), namescopeAnnotator);
        }

        private ConstructionNode GetConstructionNode(string xaml)
        {
            var sut = new XamlToTreeParser(directory, metadataProvider, new[] { new InlineParser(directory) });
            var tree = sut.Parse(xaml);
            return tree;
        }        
    }
}