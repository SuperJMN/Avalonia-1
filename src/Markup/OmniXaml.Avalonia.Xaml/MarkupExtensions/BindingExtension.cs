// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.MarkupExtensions
{
    using Data;
    using global::Avalonia;
    using global::Avalonia.Data;
    using global::Avalonia.Markup;
    using OmniXaml;

    public class BindingExtension : IMarkupExtension
    {
        public BindingExtension()
        {
        }

        public BindingExtension(string path)
        {
            Path = path;
        }

        public object GetValue(ExtensionValueContext extensionContext)
        {
            return new Binding
            {
                Converter = Converter,
                ConverterParameter = ConverterParameter,
                ElementName = ElementName,
                FallbackValue = FallbackValue,
                Mode = Mode,
                Path = Path,
                Priority = Priority,
            };
        }

        public IValueConverter Converter { get; set; }
        public object ConverterParameter { get; set; }
        public string ElementName { get; set; }
        public object FallbackValue { get; set; } = AvaloniaProperty.UnsetValue;
        public BindingMode Mode { get; set; }
        public string Path { get; set; }
        public BindingPriority Priority { get; set; } = BindingPriority.LocalValue;
        public object Source { get; set; }
    }


}