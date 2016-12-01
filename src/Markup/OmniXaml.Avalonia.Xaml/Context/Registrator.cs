namespace OmniXaml.Avalonia.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Converters;
    using global::Avalonia;
    using global::Avalonia.Collections;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Templates;
    using global::Avalonia.Markup.Xaml.Converters;
    using global::Avalonia.Media;
    using global::Avalonia.Media.Imaging;
    using global::Avalonia.Styling;
    using Glass.Core;
    using Serilog;

    public static class Registrator
    {
        public static ISourceValueConverter GetSourceValueConverter(IList<Assembly> referenceAssemblies)
        {
            var sourceValueConverter = new SourceValueConverter();

            var query = from type in referenceAssemblies.AllExportedTypes()
                        let baseType = type.GetTypeInfo().BaseType
                        where IsTypeConverter(baseType, type)
                        select type;

            foreach (var type in query.ToList())
            {
                Log.Debug("Registered converter of type {Type}", type);
                sourceValueConverter.Add((TypeConverter) Activator.CreateInstance(type));
            }

            //sourceValueConverter.Add(typeof(Thickness), context => new ThicknessTypeConverter().ConvertFrom(null, CultureInfo.CurrentCulture, context.Value));
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
            sourceValueConverter.Add(typeof(IList<Point>), context => new PointsListTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(RelativePoint), context => new RelativePointTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(Color), context => new ColorTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(RelativeRect), context => new RelativeRectTypeConverter().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));
            sourceValueConverter.Add(typeof(AvaloniaList<double>), context => new AvaloniaListTypeConverter<double>().ConvertFrom(context, CultureInfo.CurrentCulture, context.Value));

            return sourceValueConverter;
        }

        private static bool IsTypeConverter(Type baseType, Type type)
        {
            var endsWith = type.Name.EndsWith("TypeConverter");
            var hasValidLength = type.Name.Length > "TypeConverter".Length;
            var hasCorrectBaseType = baseType == typeof(TypeConverter);
            var isTypeConverter = hasCorrectBaseType && endsWith && hasValidLength;

            if (isTypeConverter)
            {
                Log.Debug("The type {Type} is a TypeConverter", type);
            }

            return isTypeConverter;
        }
    }
}