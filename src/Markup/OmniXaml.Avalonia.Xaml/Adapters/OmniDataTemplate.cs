namespace OmniXaml.Avalonia.Adapters
{
    using System;
    using global::Avalonia.Controls;
    using global::Avalonia.Controls.Templates;
    using global::Avalonia.Metadata;

    public class OmniDataTemplate : IDataTemplate
    {
        public IControl Build(object param)
        {
            return (IControl) Content.Load();
        }

        public bool Match(object data)
        {
            return true;
        }

        public Type TargetType { get; set; }

        public bool SupportsRecycling { get; } = false;

        [Content]
        public TemplateContent Content { get; set; }
    }
}