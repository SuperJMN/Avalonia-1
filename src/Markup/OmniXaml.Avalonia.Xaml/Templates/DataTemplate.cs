// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Templates
{
    using System;
    using System.Reflection;
    using Attributes;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Templates;

    public class DataTemplate : IDataTemplate
    {
        public Type DataType { get; set; }

        [Content]
        public TemplateContent Content { get; set; }

        public bool SupportsRecycling => true;

        public bool Match(object data)
        {
            if (DataType == null)
            {
                return true;
            }
            else
            {
                return DataType.GetTypeInfo().IsAssignableFrom(data.GetType().GetTypeInfo());
            }
        }

        public IControl Build(object data) => (IControl) Content.Load();
    }
}