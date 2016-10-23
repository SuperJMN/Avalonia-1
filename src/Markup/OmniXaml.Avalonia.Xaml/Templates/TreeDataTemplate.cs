// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Templates
{
    using System;
    using System.Reflection;
    using Data;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Templates;
    using global::Avalonia.Data;
    using global::Avalonia.Markup.Data;
    using global::Avalonia.Metadata;

    public class TreeDataTemplate : ITreeDataTemplate
    {
        public Type DataType { get; set; }

        [Content]
        public TemplateContent Content { get; set; }

        [AssignBinding]
        public Binding ItemsSource { get; set; }

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

        public InstancedBinding ItemsSelector(object item)
        {
            if (ItemsSource != null)
            {
                var obs = new ExpressionObserver(item, ItemsSource.Path);
                return new InstancedBinding(obs, BindingMode.OneWay, BindingPriority.Style);
            }

            return null;
        }

        public bool IsExpanded(object item)
        {
            return true;
        }

        public IControl Build(object data)
        {
            var visualTreeForItem = Content.Load();
            visualTreeForItem.DataContext = data;
            return visualTreeForItem;
        }
    }
}