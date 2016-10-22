namespace OmniXaml.Avalonia.Context
{
    using System.Reflection;
    using global::Avalonia.Controls;
    using MarkupExtensions;
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
                                typeof(BindingExtension).Namespace)));


            typeDirectory.RegisterPrefix(new PrefixRegistration(string.Empty, "root"));

            return typeDirectory;
        }

        public object Load(string xaml)
        {
            var objectBuilder = new AvaloniaObjectBuilder(new InstanceCreator(), Registrator.GetSourceValueConverter(), metadataProvider, new AvaloniaLifeCycleSignaler());
            var cons = GetConstructionNode(xaml);
            return objectBuilder.Create(cons);
        }

        private ConstructionNode GetConstructionNode(string xaml)
        {
            var sut = new XamlToTreeParser(directory, metadataProvider, new []{ new InlineParser(directory) });
            var tree = sut.Parse(xaml);
            return tree;
        }


    }
}