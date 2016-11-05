// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using global::Avalonia;
    using global::Avalonia.Styling;
    using OmniXaml;
    using Sprache;

    public class AvaloniaPropertyTypeConverter : ITypeConverter
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
            var s = (string)value;

            string typeName;
            string propertyName;
            Type type;

            ParseProperty(s, out typeName, out propertyName);

            if (typeName == null)
            {
                var style = (Style)context.BuildContext.AmbientRegistrator.Instances.Last(o => o.GetType() == typeof(Style));
                type = style.Selector?.TargetType;

                if (type == null)
                {
                    throw new ParseException(
                        "Could not determine the target type. Please fully qualify the property name.");
                }
            }
            else
            {
                type = context.TypeDirectory.GetByPrefixedName(typeName);

                if (type == null)
                {
                    throw new ParseException($"Could not find type '{typeName}'.");
                }
            }

            // First look for non-attached property on the type and then look for an attached property.
            var property = AvaloniaPropertyRegistry.Instance.FindRegistered(type, s) ??
                           AvaloniaPropertyRegistry.Instance.GetAttached(type)
                           .FirstOrDefault(x => x.Name == propertyName);

            if (property == null)
            {
                throw new ParseException(
                    $"Could not find AvaloniaProperty '{type.Name}.{propertyName}'.");
            }

            return property;
        }

        public object ConvertTo(ValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }

        private void ParseProperty(string s, out string typeName, out string propertyName)
        {
            var split = s.Split('.');

            if (split.Length == 1)
            {
                typeName = null;
                propertyName = split[0];
            }
            else if (split.Length == 2)
            {
                typeName = split[0];
                propertyName = split[1];
            }
            else
            {
                throw new ParseException($"Invalid property name: '{s}'.");
            }
        }
    }
}