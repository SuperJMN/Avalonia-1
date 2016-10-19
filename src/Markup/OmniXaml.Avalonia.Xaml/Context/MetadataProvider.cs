namespace OmniXaml.Avalonia.Context
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Adapters;
    using global::Avalonia.Controls.Templates;
    using Metadata;

    public class MetadataProvider : IMetadataProvider
    {
        public MetadataProvider()
        {            
        }

        public Metadata Get(Type type)
        {
            return new Metadata
            {
                ContentProperty = GetContentProperty(type),
                FragmentLoaderInfo = GetFragmentLoaderInfo(type),
            };
        }

        private FragmentLoadingInfo GetFragmentLoaderInfo(Type type)
        {
            if (typeof(IDataTemplate).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
                return new FragmentLoadingInfo()
                {
                    Type = type,
                    PropertyName = "Content",
                    Loader = new ConstructionFragmentLoader()
                };
            }

            return null;
        }

        private string GetContentProperty(Type type)
        {
            var contentProperty = type.GetRuntimeProperties()
               .First(info => info.GetCustomAttribute(typeof(global::Avalonia.Metadata.ContentAttribute)) != null);

            return contentProperty.Name;
        }
    }
}