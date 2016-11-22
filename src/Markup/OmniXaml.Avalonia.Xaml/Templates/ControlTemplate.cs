// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.


namespace OmniXaml.Avalonia.Templates
{
    using Attributes;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Templates;
    using global::Avalonia.Styling;

    public class ControlTemplate : IControlTemplate
    {
        [Content]
        public TemplateContent Content { get; set; }

        public IControl Build(ITemplatedControl control)
        {
            return Content.Load();
        }
    }
}