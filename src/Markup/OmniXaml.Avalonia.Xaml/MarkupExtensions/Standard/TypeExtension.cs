// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.MarkupExtensions.Standard
{
    using System;
    using global::Avalonia.Markup.Xaml.glass.Glass.Core;
    using global::Avalonia.Metadata;
    using OmniXaml;

    public class TypeExtension : IMarkupExtension
    {
        public Type Type { get; set; }

        public TypeExtension()
        {
        }

        public TypeExtension(Type type)
        {
            Type = type;
        }

        [Content]
        public string TypeName { get; set; }

        private Type ResolveFromString(string prefixedTypeName, ITypeDirectory typeDirectory)
        {
            Guard.ThrowIfNull(prefixedTypeName, nameof(prefixedTypeName));

            var tuple = prefixedTypeName.Dicotomize(':');

            if (tuple.Item2 == null)
            {
                return typeDirectory.GetTypeByPrefix(string.Empty, tuple.Item1);
            }

            return typeDirectory.GetTypeByPrefix(tuple.Item1, tuple.Item2);
        }

        public object GetValue(MarkupExtensionContext markupExtensionContext)
        {
            if (Type != null)
            {
                return Type;
            }

            return ResolveFromString(TypeName, markupExtensionContext.TypeDirectory);
        }
    }
}