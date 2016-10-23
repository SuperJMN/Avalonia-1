// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.MarkupExtensions
{
    using Data;
    using OmniXaml;

    public class StyleResourceExtension : IMarkupExtension
    {
        public StyleResourceExtension(string name)
        {
            Name = name;
        }

        public object GetValue(MarkupExtensionContext extensionContext)
        {
            return new StyleResourceBinding(this.Name);
        }

        public string Name { get; set; }
    }
}