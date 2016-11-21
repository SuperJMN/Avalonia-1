namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.Globalization;

    public class TypeTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(ConverterValueContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public object ConvertFrom(ConverterValueContext context, CultureInfo culture, object value)
        {
            var prefixedName = (string) value;
            return context.BuildContext.PrefixedTypeResolver.GetTypeByPrefix(context.BuildContext.CurrentNode, prefixedName);
        }

        public bool CanConvertTo(ConverterValueContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public object ConvertTo(
            ConverterValueContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
           throw new NotImplementedException();
        }        
    }
}