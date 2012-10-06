using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.LinqGrouping
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            List<Comparison> comparisons = new List<Comparison>()
            {
                new Comparison()
                {
                    Id = 1,
                    Quotes = new List<Quote>()
                    {
                        new Quote()
                        {
                            Id = 1,
                            Status = "Completed"
                        },
                        new Quote()
                        {
                            Id = 2,
                            Status = "Warning"
                        },
                        new Quote()
                        {
                            Id = 3,
                            Status = "Completed"
                        }
                    }
                },
                new Comparison()
                {
                    Id = 2,
                    Quotes = new List<Quote>()
                    {
                        new Quote()
                        {
                            Id = 1,
                            Status = "Error"
                        }
                    }
                },
                new Comparison()
                {
                    Id = 3,
                    Quotes = new List<Quote>()
                    {
                        new Quote()
                        {
                            Id = 1,
                            Status = "Warning"
                        },
                        new Quote()
                        {
                            Id = 2,
                            Status = "Completed"
                        },
                        new Quote()
                        {
                            Id = 3,
                            Status = "Error"
                        }
                    }
                }
            };

            var query = from c in comparisons
                        from q in c.Quotes
                        group q by q.Status;

            foreach (var item in query)
            {
                Console.WriteLine("{0} {1}", item.Key, item.Count());
            }
        }
    }
}
