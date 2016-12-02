namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class TypeTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var realContext = (ConverterValueContext) context.GetService(typeof(ConverterValueContext));

            var prefixedName = (string)value;
            var typeByPrefix = realContext.BuildContext.PrefixedTypeResolver.GetTypeByPrefix(realContext.BuildContext.CurrentNode, prefixedName);

            if (typeByPrefix == null)
            {
                throw new TypeNotFoundException($"Cannot find the type {prefixedName}");
            }

            return typeByPrefix;
        }
       
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context,CultureInfo culture,object value,Type destinationType)
        {
           throw new NotImplementedException();
        }        
    }
}