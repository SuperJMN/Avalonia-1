// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Templates
{
    using global::Avalonia.Controls;
    using global::Avalonia.Metadata;
    using global::Avalonia.Styling;

    public class Template : ITemplate<IControl>
    {
        [Content]
        public TemplateContent Content { get; set; }

        public IControl Build() => Content.Load();

        object ITemplate.Build() => Build();
    }
}