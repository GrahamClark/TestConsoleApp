using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.GraphColouring
{
    internal sealed class Solver
    {
        private readonly Graph graph;

        private readonly BitSet[] possibilities;

        private readonly int colours;

        public Solver(Graph graph, int colours)
        {
            this.colours = colours;
            this.graph = graph;
            this.possibilities = new BitSet[graph.Size];

            BitSet b = BitSet.Empty;
            for (int i = 0; i < colours; ++i)
            {
                b = b.Add(i);
            }

            for (int p = 0; p < this.possibilities.Length; ++p)
            {
                this.possibilities[p] = b;
            }
        }

        private Solver(Graph graph, int colours, BitSet[] possibilities)
        {
            this.graph = graph;
            this.colours = colours;
            this.possibilities = possibilities;
        }

        private enum Result
        {
            Solved, Unsolved, Busted
        }

        private Result Status
        {
            get
            {
                if (this.possibilities.Any(p => !p.Any()))
                {
                    return Result.Busted;
                }

                if (this.possibilities.All(p => p.Count() == 1))
                {
                    return Result.Solved;
                }

                return Result.Unsolved;
            }
        }

        /// <summary>
        /// Make a new Solver with one of the colours known.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="colour"></param>
        public Solver SetColour(int node, int colour)
        {
            BitSet[] newPossibilities = (BitSet[])this.possibilities.Clone();
            newPossibilities[node] = BitSet.Empty.Add(colour);
            return new Solver(this.graph, this.colours, newPossibilities);
        }

        public IEnumerable<int> Solve()
        {
            // Base case: we are already solved or busted.
            var status = this.Status;
            if (status == Result.Solved)
            {
                return this.possibilities.Select(x => x.Single());
            }

            if (status == Result.Busted)
            {
                return null;
            }

            // Easy inductive case: do simple reductions and then solve again.
            var reduced = this.Reduce();
            if (reduced != null)
            {
                return reduced.Solve();
            }

            // Difficult inductive case: there were no simple reductions.
            // Make a hypothesis about the colouring of a node and see if
            // that introduces a contradiction or a solution.
            int node = this.possibilities.FirstIndex(p => p.Count() > 1);
            var solutions =
                from colour in this.possibilities[node]
                let solution = this.SetColour(node, colour).Solve()
                where solution != null
                select solution;

            return solutions.FirstOrDefault();
        }

        private Solver Reduce()
        {
            BitSet[] newPossibilities = (BitSet[])this.possibilities.Clone();

            // The query answers the question "What colour possibilities should I remove?"
            var reductions =
                from node in Enumerable.Range(0, newPossibilities.Length)
                where newPossibilities[node].Count() == 1
                let colour = newPossibilities[node].Single()
                from neighbour in graph.Neighbours(node)
                where newPossibilities[neighbour].Contains(colour)
                select new { neighbour, colour };

            bool progress = false;
            while (true)
            {
                var list = reductions.ToList();
                if (list.Count == 0)
                {
                    break;
                }

                progress = true;
                foreach (var reduction in list)
                {
                    newPossibilities[reduction.neighbour] =
                        newPossibilities[reduction.neighbour].Remove(reduction.colour);
                }

                // Doing so might have created a new node that has a single possibility,
                // which we can then use to make further reductions. Keep looping until
                // there are no more reductions to be made.
            }

            return progress ? new Solver(graph, colours, newPossibilities) : null;
        }
    }
}
