namespace OmniXaml.Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Ambient;
    using Context;
    using global::Avalonia;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Html;
    using MarkupExtensions;
    using Templates;
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
                new InstanceCreator(),
                Registrator.GetSourceValueConverter(directory),
                metadataProvider);
        }

        public ConstructionResult Load(string xaml)
        {
            var objectBuilder = new AvaloniaObjectBuilder(contructionContext, (assignment, context, tc) => new ValueContext(assignment, context, directory, tc));
            var cons = GetConstructionNode(xaml);
            var namescopeAnnotator = new NamescopeAnnotator(contructionContext.MetadataProvider);
            var trackingContext = new BuildContext(namescopeAnnotator, new AmbientRegistrator(), new AvaloniaLifeCycleSignaler());
            trackingContext.Bag.Add("Uri", @"file:\\\");
            return new ConstructionResult(objectBuilder.Create(cons, trackingContext), namescopeAnnotator);
        }

        public ConstructionResult Load(string xaml, object intance)
        {
            var objectBuilder = new AvaloniaObjectBuilder(contructionContext, (assignment, context, tc) => new ValueContext(assignment, context, directory, tc));
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
                            .WithNamespaces("Avalonia.Controls", typeof(Application).Namespace),
                        Route
                            .Assembly(typeof(DataTemplate).GetTypeInfo().Assembly)
                            .WithNamespaces(
                                typeof(DataTemplate).Namespace,
                                typeof(BindingExtension).Namespace),
                        Route.Assembly(htmlControl.Assembly)
                            .WithNamespaces(htmlControl.Namespace)));
                        //Route.Assembly(typeof(StyleInclude).GetTypeInfo().Assembly)
                        //    .WithNamespaces(typeof(StyleInclude).Namespace)));


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