namespace OmniXaml.Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Ambient;
    using Context;
    using Services;
    using TypeLocation;

    public class XamlLoader : IXamlLoader
    {
        private readonly ObjectBuilderContext contructionContext;
        private readonly ITypeDirectory directory;
        private readonly MetadataProvider metadataProvider;

        public XamlLoader(IList<Assembly> referenceAssemblies)
        {
            metadataProvider = new MetadataProvider();
            directory = new AttributeBasedTypeDirectory(referenceAssemblies);

            contructionContext = new ObjectBuilderContext(
                Registrator.GetSourceValueConverter(referenceAssemblies),
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

            var trackingContext = new BuildContext(namescopeAnnotator, new AmbientRegistrator(), new AvaloniaLifeCycleSignaler())
            {
                PrefixAnnotator = cons.PrefixAnnotator,
                PrefixedTypeResolver = new PrefixedTypeResolver(cons.PrefixAnnotator, directory)
            };

            return new ConstructionResult(objectBuilder.Inflate(cons.Root, trackingContext), namescopeAnnotator);
        }

        public ConstructionResult Load(string xaml, object rootInstance)
        {
            return Load(xaml, rootInstance, null);
        }

        private ParseResult GetConstructionNode(string xaml)
        {
            var resolver = new Resolver(directory);
            var sut = new XamlToTreeParser(metadataProvider, new[] {new InlineParser(resolver) }, resolver);
            var tree = sut.Parse(xaml, new PrefixAnnotator());
            return tree;
        }

        public ConstructionResult Load(string xaml, object rootInstance, Uri uri)
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

            trackingContext.Bag.Add("Uri", CreateBaseUri(uri));

            var inflatedInstance = objectBuilder.Inflate(cons.Root, trackingContext, rootInstance);
            return new ConstructionResult(inflatedInstance, namescopeAnnotator);
        }

        private Uri CreateBaseUri(Uri uri)
        {
            var file = uri.UserInfo;
            var index = uri.OriginalString.IndexOf(file);
            return new Uri($"{uri.OriginalString.Remove(index)}");
        }
    }
}