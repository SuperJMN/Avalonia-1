// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.Globalization;
    using global::Avalonia.Controls;
    using global::Avalonia.Markup.Xaml.OmniXAML.Source.OmniXaml.ObjectAssembler;

    public class GridLengthTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(IValueContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public bool CanConvertTo(IValueContext context, Type destinationType)
        {
            return false;
        }

        public object ConvertFrom(IValueContext context, CultureInfo culture, object value)
        {
            return GridLength.Parse((string)value, culture);
        }

        public object ConvertTo(IValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }

    public interface IValueContext
    {
        TopDownValueContext TopDownValueContext { get; set; } 
    }

    public interface ITypeConverter
    {
    }
}