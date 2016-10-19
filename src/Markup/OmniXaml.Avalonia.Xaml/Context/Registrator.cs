namespace OmniXaml.Avalonia.Context
{
    using System.Globalization;
    using Converters;
    using global::Avalonia.Controls;
    using OmniXaml;

    public static class Registrator
    {
        public static ISourceValueConverter GetSourceValueConverter()
        {
            var sourceValueConverter = new SourceValueConverter();
            //sourceValueConverter.Add(typeof(Thickness), value => new ThicknessTypeConverter().ConvertFrom(null, CultureInfo.CurrentCulture, value));
            //sourceValueConverter.Add(typeof(Brush), value => new BrushTypeConverter().ConvertFrom(null, CultureInfo.CurrentCulture, value));
            sourceValueConverter.Add(typeof(GridLength), value => new GridLengthTypeConverter().ConvertFrom(null, CultureInfo.CurrentCulture, value));
            //sourceValueConverter.Add(typeof(IBitmap), value => new BitmapTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, value));
            return sourceValueConverter;
        }
    }
}