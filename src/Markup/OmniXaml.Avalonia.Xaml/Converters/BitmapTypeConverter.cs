// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Converters
{
    using System;
    using System.Globalization;
    using global::Avalonia;
    using global::Avalonia.Media.Imaging;
    using global::Avalonia.Platform;
    using OmniXaml;

    public class BitmapTypeConverter : ITypeConverter
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
            var uri = new Uri((string)context.Assignment.Value, UriKind.RelativeOrAbsolute);
            var baseUri = GetBaseUri(context);
            var scheme = uri.IsAbsoluteUri ? uri.Scheme : "file";

            switch (scheme)
            {
                case "file":
                    return new Bitmap((string)context.Assignment.Value);
                default:
                    var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                    return new Bitmap(assets.Open(uri, baseUri));
            }
        }

        public object ConvertTo(ValueContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }

        private Uri GetBaseUri(ValueContext context)
        {
            object result;
            context.TrackingContext.Bag.TryGetValue("Uri", out result);
            return result as Uri;
        }
    }    
}