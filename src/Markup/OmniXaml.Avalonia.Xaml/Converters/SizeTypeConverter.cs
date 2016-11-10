// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.Globalization;
    using global::Avalonia;

    public class SizeTypeConverter : ITypeConverter
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
            return Size.Parse((string)value, culture);
        }

        public object ConvertTo(ConverterValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}