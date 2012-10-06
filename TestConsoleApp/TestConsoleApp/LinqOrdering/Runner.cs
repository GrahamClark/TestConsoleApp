using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TestConsoleApp.LinqOrdering
{
    class Runner : IRunner
    {
        /// <summary>
        /// Ordering methods return an IOrderedEnumerable object instead of an IEnumerable.
        /// </summary>
        public void RunProgram()
        {
            var query = Process.GetProcesses()
                        .Where(p => p.ProcessName.Length < 10)
                        .OrderBy(p => p.Id)
                        .Where(p => p.ProcessName.Length < 5);

            // putting a Where after a ThenBy in the same query is ok; appending it to the query separately gives
            //  a compiler error.

            // query = query.Where(p => p.ProcessName.Length < 5);

            // this is because of the assignment - after the first assignment, query is of type IOrderedEnumerable.
            // the second assignment tries to assign an IEnumerable to an IOrderedEnumerable, which can't work.

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
    }
}
