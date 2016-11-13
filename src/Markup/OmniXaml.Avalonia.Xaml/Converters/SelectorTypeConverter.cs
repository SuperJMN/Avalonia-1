// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Globalization;

namespace Avalonia.Markup.Xaml.Converters
{
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
            var parser = new SelectorParser((t, prefix) =>
            {
                var actualPrefix = prefix ?? "";
                var typeByPrefix = context.TypeDirectory.GetTypeByPrefix(actualPrefix, t);

                if (typeByPrefix == null)
                {
                    throw new Exception($@"Cannot find the type {t} with the prefix ""{actualPrefix}""");
                }

                return typeByPrefix;
            });

            return parser.Parse((string)value);
        }

        public object ConvertTo(ConverterValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}