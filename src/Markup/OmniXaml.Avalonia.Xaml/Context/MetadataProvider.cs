﻿namespace OmniXaml.Avalonia.Context
{
    using System;
    using System.Linq;
    using System.Reflection;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Templates;
    using global::Avalonia.Platform;
    using Glass.Core;
    using Metadata;
    using Templates;

    public class MetadataProvider : IMetadataProvider
    {
        public Metadata Get(Type type)
        {
            return new Metadata
            {
                ContentProperty = GetContentProperty(type),
                FragmentLoaderInfo = GetFragmentLoaderInfo(type),
                RuntimePropertyName = GetNameProperty(type),
                IsNamescope = type.IsAssignableFrom(typeof(INameScope)),
            };
        }       

        private string GetNameProperty(Type type)
        {
            if (type.IsAssignableFrom(new[] {typeof(IControl)}))
            {
                return nameof(IControl.Name);
            }

            return null;
        }

        private FragmentLoaderInfo GetFragmentLoaderInfo(Type type)
        {
            if (type.IsAssignableFrom(typeof(IDataTemplate)))
            {
                return new FragmentLoaderInfo
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
            var contentProperties = from prop in type.GetRuntimeProperties()
                                  where prop.GetCustomAttribute<global::Avalonia.Metadata.ContentAttribute>() != null
                                  select prop.Name;
               

            return contentProperties.FirstOrDefault();
        }
    }
}