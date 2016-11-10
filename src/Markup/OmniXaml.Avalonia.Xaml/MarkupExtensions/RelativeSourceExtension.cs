// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.MarkupExtensions
{
    using Data;
    using OmniXaml;

    public class RelativeSourceExtension : IMarkupExtension
    {
        public RelativeSourceExtension()
        {
        }

        public RelativeSourceExtension(RelativeSourceMode mode)
        {
            Mode = mode;
        }

        public object GetValue(ExtensionValueContext extensionContext)
        {
            return new RelativeSource
            {
                Mode = Mode,
            };
        }

        public RelativeSourceMode Mode { get; set; }
    }
}