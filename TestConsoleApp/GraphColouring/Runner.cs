using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.GraphColouring
{
    internal class Runner : IRunner
    {
        private const int Brazil = 0;
        private const int FrenchGuiana = 1;
        private const int Suriname = 2;
        private const int Guyana = 3;
        private const int Venezuala = 4;
        private const int Colombia = 5;
        private const int Ecuador = 6;
        private const int Peru = 7;
        private const int Chile = 8;
        private const int Bolivia = 9;
        private const int Paraguay = 10;
        private const int Uruguay = 11;
        private const int Argentina = 12;

        public void RunProgram()
        {
            SolveSimpleGraph();
            Console.WriteLine("\n");
            SolveCliqueGraph();
            Console.WriteLine("\n");
            SolveSudoku();
        }

        private void SolveSudoku()
        {
            var offsets = new[,]
            {
                { new[] { 0, 9, 18, 27, 36, 45, 54, 63, 72 }, new[] { 0, 1, 2, 3, 4, 5, 7, 8 } }, // rows
                { new[] { 0, 1, 2, 3, 4, 5, 7, 8 }, new[] { 0, 9, 18, 27, 36, 45, 54, 63, 72 } }, // columns
                { new[] { 0, 3, 6, 27, 30, 33, 54, 57, 60 }, new[] { 0, 1, 2, 9, 10, 11, 18, 19, 20 } } // boxes
            };

            var cliques =
                from r in Enumerable.Range(0, 3)
                from i in offsets[r, 0]
                select (from j in offsets[r, 1] select i + j);

            var edges = CliquesToEdges(cliques);

            Graph graph = new Graph(81, edges);
            var solver = new Solver(graph, 9);
            string puzzle =
                "  8 274  " +
                "         " +
                " 6 38  52" +
                "      32 " +
                "1   7   4" +
                " 92      " +
                "78  62 1 " +
                "         " +
                "  384 5  ";

            int node = -1;
            foreach (char c in puzzle)
            {
                ++node;
                if (c == ' ')
                {
                    continue;
                }

                solver = solver.SetColour(node, c - '1');
            }

            var result = solver.Solve();
            int index = 0;
            foreach (var r in result)
            {
                Console.Write(r + 1 + " ");
                if (index % 9 == 8)
                {
                    Console.WriteLine();
                }

                ++index;
            }
        }
        
        private void SolveCliqueGraph()
        {
            var offsets = new[,]
            {
                { new[] { 0, 4, 8, 12 }, new[] { 0, 1, 2, 3 } }, // rows
                { new[] { 0, 1, 2, 3 }, new[] { 0, 4, 8, 12 } }, // columns
                { new[] { 0, 2, 8, 10 }, new[] { 0, 1, 4, 5 } } // squares
            };

            var cliques =
                from r in Enumerable.Range(0, 3)
                from i in offsets[r, 0]
                select (from j in offsets[r, 1] select i + j);

            var edges = CliquesToEdges(cliques);
            var graph = new Graph(16, edges);
            var solver = new Solver(graph, 4);
            var solution = solver.Solve();
            foreach (var colour in solution)
            {
                Console.Write(colour + ", ");
            }
        }

        private static IEnumerable<Tuple<int, int>> CliquesToEdges(IEnumerable<IEnumerable<int>> cliques)
        {
            return from clique in cliques
                   from item1 in clique
                   from item2 in clique
                   where item1 != item2
                   select Tuple.Create(item1, item2);
        }

        private static void SolveSimpleGraph()
        {
            var southAmerica = new Dictionary<int, int[]>()
            {
                { Brazil, new[] { FrenchGuiana, Suriname, Guyana, Venezuala, Colombia, Peru, Bolivia, Paraguay, Uruguay, Argentina } },
                { FrenchGuiana, new[] { Brazil, Suriname } },
                { Suriname, new[] { Brazil, FrenchGuiana, Guyana } },
                { Guyana, new[] { Brazil, Suriname, Venezuala } },
                { Venezuala, new[] { Brazil, Guyana, Colombia } },
                { Colombia, new[] { Brazil, Venezuala, Peru, Ecuador } },
                { Ecuador, new[] { Colombia, Peru } },
                { Peru, new[] { Brazil, Colombia, Ecuador, Bolivia, Chile } },
                { Chile, new[] { Peru, Bolivia, Argentina } },
                { Bolivia, new[] { Chile, Peru, Brazil, Paraguay, Argentina } },
                { Paraguay, new[] { Bolivia, Brazil, Argentina } },
                { Argentina, new[] { Chile, Bolivia, Paraguay, Brazil, Uruguay } },
                { Uruguay, new[] { Brazil, Argentina } }
            };

            var southAmericaGraph = new Graph(13, from x in southAmerica.Keys
                                                  from y in southAmerica[x]
                                                  select Tuple.Create(x, y));

            var southAmericaSolver = new Solver(southAmericaGraph, 4);
            var solution = southAmericaSolver.Solve();
            foreach (var colour in solution)
            {
                Console.Write(colour + ", ");
            }
        }
    }
}
