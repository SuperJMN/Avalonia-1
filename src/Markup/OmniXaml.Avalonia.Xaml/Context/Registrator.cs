namespace OmniXaml.Avalonia.Context
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Converters;
    using global::Avalonia;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Templates;
    using global::Avalonia.Markup.Xaml.Converters;
    using global::Avalonia.Media;
    using global::Avalonia.Media.Imaging;
    using global::Avalonia.Styling;
    using Tests.Namespaces;

    public static class Registrator
    {
        public static ISourceValueConverter GetSourceValueConverter(ITypeDirectory typeDirectory)
        {
            var sourceValueConverter = new SourceValueConverter();

            sourceValueConverter.Add(typeof(Thickness), context => new ThicknessTypeConverter().ConvertFrom(null, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(Brush), context => new BrushTypeConverter().ConvertFrom(null, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(GridLength), context => new GridLengthTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(ColumnDefinitions), context => new ColumnDefinitionsTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(RowDefinitions), context => new RowDefinitionsTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(IBitmap), context => new BitmapTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(Uri), context => new UriTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(Selector), context => new SelectorTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(IMemberSelector), context => new MemberSelectorTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(AvaloniaProperty), context => new AvaloniaPropertyTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(TimeSpan), context => new TimeSpanTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(IBrush), context => new BrushTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(Geometry), context => new GeometryTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(Point), context => new PointTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(List<Point>), context => new PointsListTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(RelativePoint), context => new RelativePointTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(Color), context => new ColorTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(RelativeRect), context => new RelativeRectTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(Type), context => new TypeTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));

            return sourceValueConverter;
        }
    }
}