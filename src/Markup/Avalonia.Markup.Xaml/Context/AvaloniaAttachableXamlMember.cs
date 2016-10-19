// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Avalonia.Markup.Xaml.Data;
using System.Reflection;

namespace Avalonia.Markup.Xaml.Context
{
    using OmniXAML.Source.OmniXaml;
    using OmniXAML.Source.OmniXaml.Typing;

    public class AvaloniaAttachableXamlMember : AttachableMember
    {
        public AvaloniaAttachableXamlMember(string name,
            XamlType owner,
            MethodInfo getter,
            MethodInfo setter,
            ITypeRepository xamlTypeRepository,
            ITypeFeatureProvider featureProvider)
            : base(name, getter, setter, xamlTypeRepository, featureProvider)
        {
        }

        public override string ToString()
        {
            return "Avalonia Attachable XAML Member " + base.ToString();
        }

        protected override IMemberValuePlugin LookupXamlMemberValueConnector()
        {
            return new AvaloniaMemberValuePlugin(this);
        }
    }
}