﻿namespace OmniXaml.Avalonia.Context
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using global::Avalonia.Controls;
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
                IsNamescope = type.IsAssignableFrom(typeof(INameScope), typeof(ITemplate<>)) || type.GetTypeInfo().ImplementedInterfaces.Any(type1 => type.Name.Contains("Template")),
                PropertyDependencies = GetDependencyRegistrations(type),
            };
        }


        private DependencyRegistrations GetDependencyRegistrations(Type type)
        {
            var regs = type.GetAttributesFromProperties<DependsOnAttribute, DependencyRegistration>(GetDependencyRegistration);
            return new DependencyRegistrations(regs);
        }

        private DependencyRegistration GetDependencyRegistration(PropertyInfo info, DependsOnAttribute attribute)
        {
            return new DependencyRegistration
            {
                PropertyName = info.Name,
                DependsOn = attribute.Dependency,                
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
            if (type.GetTypeInfo().ImplementedInterfaces.Any(type1 => type.Name.Contains("Template")))
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
                                  where prop.GetCustomAttribute<ContentAttribute>() != null
                                  select prop.Name;
                           
            return contentProperties.FirstOrDefault();
        }
    }
}