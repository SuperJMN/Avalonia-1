namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.Globalization;
    using global::Avalonia.Media;

    public class FontWeightConverter : ITypeConverter
    {
        public bool CanConvertFrom(ValueContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public bool CanConvertTo(ValueContext context, Type destinationType)
        {
            return false;
        }

        public object ConvertFrom(ValueContext context, CultureInfo culture, object value)
        {
            FontWeight result;
            
            if (Enum.TryParse(value as string, out result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("unable to convert parameter to FontWeight");
            }
        }

        public object ConvertTo(ValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}
