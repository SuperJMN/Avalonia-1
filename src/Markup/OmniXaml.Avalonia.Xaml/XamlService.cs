namespace OmniXaml.Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Data;
    using global::Avalonia;
    using global::Avalonia.Controls;
    using global::Avalonia.Markup;
    using global::Avalonia.Media;
    using global::Avalonia.Platform;
    using global::Avalonia.Styling;
    using OmniXaml;
    using Styling;
    using Templates;
    
    public class XamlService
    {
        private static XamlService s_current;

        public static XamlService Current
        {
            get { return s_current ?? (s_current = new XamlService()); }
            set { s_current = value; }
        }

        private readonly XamlLoader loader;

        public XamlService()
        {
            loader = new XamlLoader(GetReferenceAssemblies());
        }

        private static IList<Assembly> GetReferenceAssemblies()
        {
            IEnumerable<Assembly> forcedAssemblies = new[]
            {
                typeof(AvaloniaObject).GetTypeInfo().Assembly,
                typeof(Control).GetTypeInfo().Assembly,
                typeof(Style).GetTypeInfo().Assembly,
                typeof(DataTemplate).GetTypeInfo().Assembly,
                typeof(SolidColorBrush).GetTypeInfo().Assembly,
                typeof(IValueConverter).GetTypeInfo().Assembly,
                typeof(StyleInclude).GetTypeInfo().Assembly,
                //typeof(HtmlLabel).GetTypeInfo().Assembly,
            };

            var runtimePlatform = AvaloniaLocator.Current.GetService<IRuntimePlatform>();

            var loadedAssemblies = runtimePlatform?.GetLoadedAssemblies() ?? new Assembly[0];

            var scanned = loadedAssemblies.Except(forcedAssemblies);

            return forcedAssemblies.Concat(scanned).ToList();
        }

        public ConstructionResult LoadUri(Uri uri, object rootInstance = null)
        {
            var assetLocator = AvaloniaLocator.Current.GetService<IAssetLoader>();

            using (var stream = assetLocator.Open(uri))
            {
                var initialize = rootInstance as ISupportInitialize;
                initialize?.BeginInit();
                return Load(stream, rootInstance, uri);
            }
        }

        public ConstructionResult Load(object rootInstance)
        {
            var assetLocator = AvaloniaLocator.Current.GetService<IAssetLoader>();

            if (assetLocator == null)
            {
                throw new InvalidOperationException(
                    "Could not create IAssetLoader : maybe Application.RegisterServices() wasn't called?");
            }

            Type type = rootInstance.GetType();
            foreach (var uri in GetUrisFor(type))
            {
                if (assetLocator.Exists(uri))
                {
                    using (var stream = assetLocator.Open(uri))
                    {
                        var initialize = rootInstance as ISupportInitialize;
                        initialize?.BeginInit();
                        return Load(stream, rootInstance, uri);
                    }
                }
            }

            throw new FileNotFoundException("Unable to find view for " + type.FullName);
        }

        private ConstructionResult Load(Stream stream, object rootInstance = null, Uri uri = null)
        {
            var constructionResult = loader.Load(new StreamReader(stream).ReadToEnd(), rootInstance, uri);

            var topLevel = constructionResult.Instance as TopLevel;

            if (topLevel != null)
            {
                DelayedBinding.ApplyBindings(topLevel);
            }

            return constructionResult;
        }

        private IEnumerable<Uri> GetUrisFor(Type type)
        {
            var asm = type.GetTypeInfo().Assembly.GetName().Name;
            var typeName = type.Namespace + "." + type.Name;
            var path = typeName.Replace(".", "/");
            var i = asm.Count() + 1;
            path = path.Remove(0, i);
            yield return new Uri($"resm:[{asm}]/{path}.xaml" );
            yield return new Uri($"resm:[{asm}]/{path}.paml");

        }
    }
}