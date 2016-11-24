using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;

namespace ControlCatalog.Pages
{
    using OmniXaml.Avalonia;

    public class TreeViewPage : UserControl
    {
        public TreeViewPage()
        {
            this.InitializeComponent();
            DataContext = CreateNodes(0);
        }

        private void InitializeComponent()
        {
            XamlService.Current.Load(this);
        }

        private IList<Node> CreateNodes(int level)
        {
            return Enumerable.Range(0, 10).Select(x => new Node
            {
                Header = $"Item {x}",
                Children = level < 5 ? CreateNodes(level + 1) : null,
            }).ToList();
        }

        private class Node
        {
            public string Header { get; set; }
            public IList<Node> Children { get; set; }
        }
    }
}
