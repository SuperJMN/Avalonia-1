// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Globalization;

namespace Avalonia.Markup.Xaml.Converters
{
    using System.Diagnostics;
    using OmniXaml;
    using OmniXaml.Avalonia.Converters;
    using OmniXaml.Avalonia.Parsers;

    public class SelectorTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(ConverterValueContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public bool CanConvertTo(ConverterValueContext context, Type destinationType)
        {
            return false;
        }

        public object ConvertFrom(ConverterValueContext context, CultureInfo culture, object value)
        {
            var parser = new SelectorParser((typeName, prefix) => ResolveTypeFromPrefix(context, prefix, typeName));

            return parser.Parse((string)value);
        }

        private static Type ResolveTypeFromPrefix(ConverterValueContext context, string prefix, string typeName)
        {
            var finalPrefix = prefix ?? string.Empty;
            var prefixedType = finalPrefix + ":" + typeName;

            var typeByPrefix = context.BuildContext.PrefixedTypeResolver.GetTypeByPrefix(context.BuildContext.CurrentNode, prefixedType);

            if (typeByPrefix == null)
            {
                throw new Exception($@"Cannot find the type '{prefixedType}'");
            }

            return typeByPrefix;
        }

        public object ConvertTo(ConverterValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}