using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.GenericsHashing
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            Dictionary<string, string> items = Hasher.Hash(Name => "joe", Age => "10", Height => (70 * 2).ToString());

            var q = from i in items
                    where i.Value.Length > 2
                    select new { i.Key, i.Value };

            var r = items.Where(j => j.Value.Length > 2).Select(n => new { n.Key, n.Value });

            foreach (var item in q)
            {
                Console.WriteLine(string.Format("{0}:\t{1}", item.Key, item.Value));
            }
            Console.WriteLine();
            foreach (var item in r)
            {
                Console.WriteLine(string.Format("{0}:\t{1}", item.Key, item.Value));
            }
        }
    }
}
