using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.TreeDisplay
{
    class Node
    {
        public Node(string text, params Node[] children)
        {
            this.Text = text;
            this.Children = children ?? new Node[] { };
        }

        public string Text { get; private set; }

        public IList<Node> Children { get; private set; }
    }
}
