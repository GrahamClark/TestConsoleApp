using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.SortingCountries
{
    class Runner : IRunner
    {
        IEnumerable<A> countries = new[]
        {
            new A { Key = 0, Value = "Afghanistan" },
            new A { Key = 1, Value = "Bulgaria" },
            new A { Key = 2, Value = "United Kingdom" },
            new A { Key = 3, Value = "United States" },
            new A { Key = 4, Value = "Zimbabwe" }
        };

        public void RunProgram()
        {
            foreach (var country in SortCountries(countries, new[] { "United Kingdom", "United States" }))
            {
                Console.WriteLine(country.Value);
            }
        }

        private IEnumerable<A> SortCountries(
            IEnumerable<A> countries,
            IEnumerable<string> topCountries)
        {
            List<A> result = new List<A>();
            foreach (var item in topCountries)
            {
                var match = countries.Where(c => c.Value == item).FirstOrDefault();
                if (match != null)
                {
                    result.Add(match);
                }
            }

            return result.Concat(countries.OrderBy(c => c.Value));
        }
    }

    class A
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
