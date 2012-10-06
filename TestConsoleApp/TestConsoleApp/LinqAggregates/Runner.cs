using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsoleApp.LinqGrouping;

namespace TestConsoleApp.LinqAggregates
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            ComplexTest();
        }

        private static void ComplexTest()
        {
            List<Comparison> comparisons = GetTestList();

            foreach (var c in comparisons)
            {
                foreach (var q in c.Quotes)
                {
                    if (q.CompletedDate.HasValue)
                    {
                        Console.WriteLine("Quote Time = " + (q.CompletedDate.Value - q.CreatedDate).TotalMilliseconds);
                    }
                }
            }
            Console.WriteLine();

            var query = from c in comparisons
                        from q in c.Quotes
                        select new { C = c, Q = q };

            var averageQuoteTime = query.Average(x => x.Q.CompletedDate.HasValue ? (x.Q.CompletedDate.Value - x.Q.CreatedDate).TotalMilliseconds : 0);
            Console.WriteLine(averageQuoteTime);

            var query2 = from i in Enumerable.Repeat(0, 1)
                         from c in comparisons
                         from q in c.Quotes
                         select new { i, c, q } into x
                         group x by x.i into g
                         select new
                         {
                             Average1 = g.Average(x => x.q.CompletedDate.HasValue ? (x.q.CompletedDate.Value - x.c.CreatedDate).TotalMilliseconds : 0),
                             Average2 = g.Average(x => x.q.CompletedDate.HasValue ? (x.q.CompletedDate.Value - x.q.CreatedDate).TotalMilliseconds : 0)
                         };

            foreach (var item in query2)
            {
                Console.WriteLine("Average1 = " + item.Average1);
                Console.WriteLine("Average2 = " + item.Average2);
            }
        }

        private static void SimpleTest()
        {
            List<int> numbers = new List<int>() { 1, 3, 6, 2, 5, 8, 4 };

            var query = numbers.Aggregate(
                            new { Min = int.MaxValue, Max = int.MinValue },
                            (a, i) =>
                                new
                                {
                                    Min = (i < a.Min) ? i : a.Min,
                                    Max = (a.Max < i) ? i : a.Max
                                });

            Console.WriteLine("{0} {1}", query.Min, query.Max);
        }

        private static List<Comparison> GetTestList()
        {
            return new List<Comparison>()
            {
                new Comparison()
                {
                    Id = 1,
                    CreatedDate = new DateTime(2009, 6, 1, 15, 1, 0),
                    Quotes = new List<Quote>()
                    {
                        new Quote()
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2009, 6, 1, 15, 1, 1),
                            CompletedDate = new DateTime(2009, 6, 1, 15, 1, 10),
                            Status = "Completed"
                        },
                        new Quote()
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2009, 6, 1, 15, 1, 2),
                            CompletedDate = new DateTime(2009, 6, 1, 15, 1, 8),
                            Status = "Warning"
                        },
                        new Quote()
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2009, 6, 1, 15, 1, 2),
                            CompletedDate = new DateTime(2009, 6, 1, 15, 1, 4),
                            Status = "Completed"
                        }
                    }
                },
                new Comparison()
                {
                    Id = 2,
                    CreatedDate = new DateTime(2009, 6, 1, 15, 10, 20),
                    Quotes = new List<Quote>()
                    {
                        new Quote()
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2009, 6, 1, 15, 10, 23),
                            CompletedDate = null,
                            Status = "Error"
                        }
                    }
                },
                new Comparison()
                {
                    Id = 3,
                    CreatedDate = new DateTime(2009, 6, 2, 14, 0, 0),
                    Quotes = new List<Quote>()
                    {
                        new Quote()
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2009, 6, 2, 14, 0, 1),
                            CompletedDate = new DateTime(2009, 6, 2, 14, 0, 2),
                            Status = "Warning"
                        },
                        new Quote()
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2009, 6, 2, 14, 0, 2),
                            CompletedDate = new DateTime(2009, 6, 2, 14, 0, 6),
                            Status = "Completed"
                        },
                        new Quote()
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2009, 6, 2, 14, 0, 2),
                            CompletedDate = new DateTime(2009, 6, 2, 14, 0, 10),
                            Status = "Error"
                        }
                    }
                }
            };
        }
    }
}
