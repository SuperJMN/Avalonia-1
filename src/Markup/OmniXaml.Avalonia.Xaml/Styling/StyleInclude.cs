// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniXaml.Avalonia.Styling
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Avalonia;
    using global::Avalonia;
    using global::Avalonia.Platform;
    using global::Avalonia.Styling;

    /// <summary>
    /// Includes a style from a URL.
    /// </summary>
    public class StyleInclude : IStyle
    {
        private Uri baseUri;
        private IStyle loaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleInclude"/> class.
        /// </summary>
        public StyleInclude()
        {
            // StyleInclude will usually be loaded from XAML and its URI can be relative to the
            // XAML file that its included in, so store the current XAML file's URI if any as
            // a base URI.
        }

        /// <summary>
        /// Gets or sets the source URL.
        /// </summary>
        public Uri Source { get; set; }

        /// <summary>
        /// Gets the loaded style.
        /// </summary>
        public IStyle Loaded
        {
            get
            {
                if (loaded == null)
                {
                    loaded = (IStyle)XamlService.Current.LoadUri(Source).Instance;                                       
                }

                return loaded;
            }
        }

        /// <inheritdoc/>
        public void Attach(IStyleable control, IStyleHost container)
        {
            if (Source != null)
            {
                 Loaded.Attach(control, container);
            }
        }

        /// <summary>
        /// Tries to find a named resource within the style.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <returns>
        /// The resource if found, otherwise <see cref="AvaloniaProperty.UnsetValue"/>.
        /// </returns>
        public object FindResource(string name)
        {
            return Loaded.FindResource(name);
        }
    }
}
