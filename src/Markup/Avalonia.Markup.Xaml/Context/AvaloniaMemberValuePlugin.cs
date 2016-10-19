// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;

namespace Avalonia.Markup.Xaml.Context
{
    using OmniXAML.Source.OmniXaml.TypeConversion;
    using OmniXAML.Source.OmniXaml.Typing;

    public class AvaloniaMemberValuePlugin : MemberValuePlugin
    {
        private readonly MutableMember _xamlMember;

        public AvaloniaMemberValuePlugin(MutableMember xamlMember) 
            : base(xamlMember)
        {
            _xamlMember = xamlMember;
        }

        public override void SetValue(object instance, object value, IValueContext valueContext)
        {
            PropertyAccessor.SetValue(instance, _xamlMember, value, valueContext);
        }
    }
}
