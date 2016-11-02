namespace OmniXaml.Avalonia.Context
{
    using System.Globalization;
    using Converters;
    using global::Avalonia.Controls;
    using global::Avalonia.Media.Imaging;
    using OmniXaml;

    public static class Registrator
    {
        public static ISourceValueConverter GetSourceValueConverter(ITypeDirectory typeDirectory)
        {
            var sourceValueConverter = new SourceValueConverter();

            //sourceValueConverter.Add(typeof(Thickness), value => new ThicknessTypeConverter().ConvertFrom(null, CultureInfo.CurrentCulture, value));
            //sourceValueConverter.Add(typeof(Brush), value => new BrushTypeConverter().ConvertFrom(null, CultureInfo.CurrentCulture, value));
            sourceValueConverter.Add(typeof(GridLength), (value) => new GridLengthTypeConverter().ConvertFrom(value, CultureInfo.CurrentCulture, value));
            sourceValueConverter.Add(typeof(ColumnDefinitions), (value) => new ColumnDefinitionsTypeConverter().ConvertFrom(value, CultureInfo.CurrentCulture, value));
            sourceValueConverter.Add(typeof(RowDefinitions), (value) => new RowDefinitionsTypeConverter().ConvertFrom(value, CultureInfo.CurrentCulture, value));
            sourceValueConverter.Add(typeof(IBitmap), value => new BitmapTypeConverter().ConvertFrom(value, CultureInfo.CurrentCulture, value));
            return sourceValueConverter;
        }
    }

   
}