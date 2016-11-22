// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.MarkupExtensions
{
    using System;
    using Attributes;
    using Glass.Core;
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

        private Type ResolveFromString(string prefixedTypeName, ExtensionValueContext markupExtensionContext)
        {
            Guard.ThrowIfNull(prefixedTypeName, nameof(prefixedTypeName));

            var prefixedType = (string)markupExtensionContext.Assignment.Value;
            return markupExtensionContext.BuildContext.PrefixedTypeResolver.GetTypeByPrefix(markupExtensionContext.BuildContext.CurrentNode, prefixedType);
        }

        public object GetValue(ExtensionValueContext markupExtensionContext)
        {
            if (Type != null)
            {
                return Type;
            }

            return ResolveFromString(TypeName, markupExtensionContext);
        }
    }
}