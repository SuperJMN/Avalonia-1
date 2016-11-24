// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.Globalization;

    public class UriTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(ConverterValueContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public bool CanConvertTo(ConverterValueContext context, Type destinationType)
        {
            return false;
        }

        public object ConvertFrom(ConverterValueContext context, CultureInfo culture, object value)
        {
            Uri uri;
            var tryAbsolute = new Uri((string)value, UriKind.RelativeOrAbsolute);
            if (tryAbsolute.IsAbsoluteUri)
            {
                uri = tryAbsolute;
            }
            else
            {
                object absolutelyUri;
                if (context.BuildContext.Bag.TryGetValue("Uri", out absolutelyUri))
                {
                    var baseUri = (Uri)absolutelyUri;
                    var relativeUri = new Uri((string)value, UriKind.Relative);

                    var finalUri = new Uri(baseUri, relativeUri);

                    uri = finalUri;
                }
                else
                {
                    throw new InvalidOperationException($"Cannot get the base Uri for {value}");
                }
            }

            if (!uri.IsWellFormedOriginalString())
            {
                throw new InvalidOperationException($"Invalid Uri {value}");
            }
            return uri;
        }

        public object ConvertTo(ConverterValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}