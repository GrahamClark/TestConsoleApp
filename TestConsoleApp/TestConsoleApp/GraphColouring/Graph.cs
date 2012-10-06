using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.GraphColouring
{
    /// <summary>
    /// An immutable directed graph.
    /// </summary>
    internal sealed class Graph
    {
        /// <summary>
        /// If there is an edge from A to B then neighbours[A,B] is true.
        /// </summary>
        private readonly bool[,] neighbours;

        private readonly int nodes;

        public Graph(int nodes, IEnumerable<Tuple<int, int>> edges)
        {
            this.nodes = nodes;
            this.neighbours = new bool[nodes, nodes];
            foreach (var edge in edges)
            {
                this.neighbours[edge.Item1, edge.Item2] = true;
            }
        }

        public int Size
        {
            get { return this.nodes; }
        }

        public IEnumerable<int> Neighbours(int node)
        {
            return from i in Enumerable.Range(0, this.Size)
                   where this.neighbours[node, i]
                   select i;
        }
    }
}
