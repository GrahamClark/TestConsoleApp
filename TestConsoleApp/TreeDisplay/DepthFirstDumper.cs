using System;
using System.Text;

namespace TestConsoleApp.TreeDisplay
{
    sealed class DepthFirstDumper
    {
        public static string Dump(Node root)
        {
            StringBuilder nodeDump = new StringBuilder();
            Dump(root, nodeDump, String.Empty);

            return nodeDump.ToString();
        }

        private static void Dump(Node node, StringBuilder nodeDump, string prefix)
        {
            nodeDump.AppendLine(node.Text);
            for (int i = 0; i < node.Children.Count; i++)
            {
                bool isLastChild = i + 1 == node.Children.Count;
                nodeDump.Append(prefix);
                nodeDump.Append(isLastChild ? '└' : '├');
                nodeDump.Append('─');

                Dump(node.Children[i], nodeDump, prefix + (isLastChild ? "  " : "│ "));
            }
        }
    }
}
