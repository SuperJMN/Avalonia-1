// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using global::Avalonia;

    public class ThicknessTypeConverter : TypeConverter
    {
        public ThicknessTypeConverter()
        {
            
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return Thickness.Parse((string)value, culture);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}