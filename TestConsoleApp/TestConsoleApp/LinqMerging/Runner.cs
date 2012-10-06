using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.LinqMerging
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var first = new List<Item> { new Item { Name = "one", Count = 2, Price = 2.30m }, new Item { Name = "two", Count = 3, Price = 5.70m } };
            var second = new List<Item> { new Item { Name = "two", Count = 4, Price = 1.75m }, new Item { Name = "three", Count = 1, Price = 2.25m } };
            var third = new List<Item> { new Item { Name = "one", Count = 6, Price = 5.50m } };

            // we want to combine these lists into one that looks like this:
            // { { "one", 8, 7.80 }, { "two", 7, 7.45 }, { "three", 1, 2.25 } }

            var query1 = first.Concat(second)
                              .Concat(third)
                              .GroupBy(i => i.Name)
                              .Select(g => new Item { Name = g.Key, Count = g.Sum(i => i.Count), Price = g.Sum(i => i.Price) });

            var query2 = first.Concat(second)
                              .Concat(third)
                              .GroupBy(i => i.Name, (key, groupedItems) => new Item { Name = key, Count = groupedItems.Sum(i => i.Count), Price = groupedItems.Sum(i => i.Price) });

            var query3 = first.Concat(second)
                              .Concat(third)
                              .GroupBy(i => i.Name)
                              .Select(g => g.Aggregate((f, s) => new Item { Name = f.Name, Count = f.Count + s.Count, Price = f.Price + s.Price }));

            foreach (var item in query1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            foreach (var item in query2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            foreach (var item in query3)
            {
                Console.WriteLine(item);
            }
        }
    }

    class Item
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return String.Format("{0} : {1}, £{2}", this.Name, this.Count, this.Price.ToString("F2"));
        }
    }
}
