// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace Avalonia.Markup.Xaml.Context
{
    using OmniXamlV1;
    using OmniXamlV1.Typing;

    public class AvaloniaXamlMember : Member
    {
        public AvaloniaXamlMember(string name,
            XamlType owner,
            ITypeRepository xamlTypeRepository,
            ITypeFeatureProvider featureProvider)
            : base(name, owner, xamlTypeRepository, featureProvider)
        {
        }

        protected override IMemberValuePlugin LookupXamlMemberValueConnector()
        {
            return new AvaloniaMemberValuePlugin(this);
        }

        public override string ToString()
        {
            return "Avalonia XAML Member " + base.ToString();
        }
    }
}