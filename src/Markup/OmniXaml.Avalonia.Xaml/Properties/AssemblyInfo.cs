// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System.Reflection;
using System.Runtime.CompilerServices;
using OmniXaml.Attributes;

[assembly: AssemblyTitle("OmniXaml.Avalonia.Xaml")]
[assembly: InternalsVisibleTo("Avalonia.Markup.Xaml.UnitTests")]

[assembly: XmlnsDefinition("https://github.com/avaloniaui", "OmniXaml.Avalonia.Templates")]
[assembly: XmlnsDefinition("https://github.com/avaloniaui", "OmniXaml.Avalonia.MarkupExtensions")]
[assembly: XmlnsDefinition("https://github.com/avaloniaui", "OmniXaml.Avalonia.Styling")]
[assembly: XmlnsDefinition("https://github.com/avaloniaui", "OmniXaml.Avalonia.Data")]

