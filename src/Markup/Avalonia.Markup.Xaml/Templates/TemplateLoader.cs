// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System.Collections.Generic;

namespace Avalonia.Markup.Xaml.Templates
{
    using OmniXAML.Source.OmniXaml;

    public class TemplateLoader : IDeferredLoader
    {
        public object Load(IEnumerable<Instruction> nodes, IRuntimeTypeSource runtimeTypeSource)
        {
            return new TemplateContent(nodes, runtimeTypeSource);
        }
    }
}