namespace AvaloniaApp
{
    using System.Collections.ObjectModel;

    internal class MainViewModel
    {
        public MainViewModel()
        {
            Title = "Título";
            Items = new Collection<string> { "hola", "tío", "cómo estás?" };
        }
        public string Title { get; set; }

        public Collection<string> Items { get; set; }
    }
}