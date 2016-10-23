namespace OmniXaml.Avalonia.Context
{
    using System.Reflection;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Html;
    using global::Avalonia.Markup.Xaml.MarkupExtensions;
    using Templates;
    using TypeLocation;

    public class XamlLoader
    {
        private readonly TypeDirectory directory;
        private readonly MetadataProvider metadataProvider;

        public XamlLoader()
        {
            metadataProvider = new MetadataProvider();
            directory = RegisterTypeLocation();
        }

        private TypeDirectory RegisterTypeLocation()
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
                                typeof(MarkupExtensions.BindingExtension).Namespace,
                                typeof(MarkupExtensions.Standard.TypeExtension).Namespace),
                        Route.Assembly(htmlControl.Assembly)
                            .WithNamespaces(htmlControl.Namespace)));


            typeDirectory.RegisterPrefix(new PrefixRegistration(string.Empty, "root"));

            return typeDirectory;
        }

        public object Load(string xaml)
        {
            var contructionContext = new ConstructionContext(
                new InstanceCreator(),
                Registrator.GetSourceValueConverter(),
                metadataProvider,
                new AvaloniaLifeCycleSignaler());

            var objectBuilder =
                new AvaloniaObjectBuilder(contructionContext, (assignment, context) => new MarkupExtensionContext(assignment, contructionContext, directory));
            var cons = GetConstructionNode(xaml);
            return objectBuilder.Create(cons);
        }

        private ConstructionNode GetConstructionNode(string xaml)
        {
            var sut = new XamlToTreeParser(directory, metadataProvider, new[] { new InlineParser(directory) });
            var tree = sut.Parse(xaml);
            return tree;
        }


    }
}