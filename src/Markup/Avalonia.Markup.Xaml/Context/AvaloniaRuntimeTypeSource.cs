namespace Avalonia.Markup.Xaml.Context
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXamlV1;
    using OmniXamlV1.Builder;
    using OmniXamlV1.Typing;

    public class AvaloniaRuntimeTypeSource : IRuntimeTypeSource
    {
        private readonly RuntimeTypeSource inner;

        public AvaloniaRuntimeTypeSource(ITypeFactory typeFactory)
        {
            var namespaceRegistry = new AvaloniaNamespaceRegistry();
            var featureProvider = new AvaloniaTypeFeatureProvider();
            var typeRepository = new AvaloniaTypeRepository(namespaceRegistry, typeFactory, featureProvider);

            inner = new RuntimeTypeSource(typeRepository, namespaceRegistry);
        }

        public Namespace GetNamespace(string name)
        {
            return inner.GetNamespace(name);
        }

        public Namespace GetNamespaceByPrefix(string prefix)
        {
            return inner.GetNamespaceByPrefix(prefix);
        }

        public void RegisterPrefix(PrefixRegistration prefixRegistration)
        {
            inner.RegisterPrefix(prefixRegistration);
        }

        public void AddNamespace(XamlNamespace xamlNamespace)
        {
            inner.AddNamespace(xamlNamespace);
        }

        public IEnumerable<PrefixRegistration> RegisteredPrefixes => inner.RegisteredPrefixes;

        public XamlType GetByType(Type type)
        {
            return inner.GetByType(type);
        }

        public XamlType GetByQualifiedName(string qualifiedName)
        {
            return inner.GetByQualifiedName(qualifiedName);
        }

        public XamlType GetByPrefix(string prefix, string typeName)
        {
            return inner.GetByPrefix(prefix, typeName);
        }

        public XamlType GetByFullAddress(XamlTypeName xamlTypeName)
        {
            return inner.GetByFullAddress(xamlTypeName);
        }

        public Member GetMember(PropertyInfo propertyInfo)
        {
            return inner.GetMember(propertyInfo);
        }

        public AttachableMember GetAttachableMember(string name, MethodInfo getter, MethodInfo setter)
        {
            return inner.GetAttachableMember(name, getter, setter);
        }
    }
}