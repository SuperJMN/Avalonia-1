﻿namespace OmniXaml.Avalonia.Context
{
    using System;
    using System.Globalization;
    using Converters;
    using global::Avalonia;
    using global::Avalonia.Controls;
    using global::Avalonia.Markup.Xaml.Converters;
    using global::Avalonia.Media;
    using global::Avalonia.Media.Imaging;
    using global::Avalonia.Styling;
    using Templates;

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
            sourceValueConverter.Add(typeof(MemberSelector), context => new MemberSelectorTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(AvaloniaProperty), context => new AvaloniaPropertyTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            return sourceValueConverter;
        }
    }
}