namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.Globalization;
    using global::Avalonia.Input;

    class KeyGestureConverter : ITypeConverter
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
            return KeyGesture.Parse((string)value);
        }

        public object ConvertTo(IValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}
