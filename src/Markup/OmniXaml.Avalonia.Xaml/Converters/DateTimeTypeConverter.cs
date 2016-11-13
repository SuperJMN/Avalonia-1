﻿namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.Globalization;

    public class DateTimeTypeConverter : ITypeConverter
    {
        public object ConvertFrom(ConverterValueContext context, CultureInfo culture, object value)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
            DateTime d = DateTime.ParseExact(value.ToString(), dateTimeFormatInfo.ShortDatePattern, culture);
            return d;
        }

        public object ConvertTo(ConverterValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            DateTime? d = value as DateTime?;

            if (!d.HasValue || destinationType != typeof(string))
            {
                throw new NotSupportedException();
            }
            DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
            return d.Value.ToString(dateTimeFormatInfo.ShortDatePattern, culture);
        }

        public bool CanConvertTo(ConverterValueContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public bool CanConvertFrom(ConverterValueContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }
    }
}