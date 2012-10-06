using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.LoopVariables
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            FirstAttempt();
            Console.WriteLine();

            CorrectMethod();
            Console.WriteLine();

            ExpansionOfFirst();
        }

        private static void FirstAttempt()
        {
            var values = new List<int> { 100, 110, 120 };
            var funcs = new List<Func<int>>();

            foreach (var v in values)
            {
                funcs.Add(() => v);
            }

            foreach (var f in funcs)
            {
                Console.WriteLine(f());
                // prints 120 120 120
            }
        }

        private static void CorrectMethod()
        {
            var values = new List<int> { 100, 110, 120 };
            var funcs = new List<Func<int>>();

            foreach (var v in values)
            {
                var v2 = v;
                funcs.Add(() => v2);
            }

            foreach (var f in funcs)
            {
                Console.WriteLine(f());
                // prints 100 110 120
            }
        }

        private static void ExpansionOfFirst()
        {
            var values = new List<int> { 100, 110, 120 };
            var funcs = new List<Func<int>>();

            IEnumerator<int> e = ((IEnumerable<int>)values).GetEnumerator();
            try
            {
                int m;
                while (e.MoveNext())
                {
                    m = (int)e.Current;
                    funcs.Add(() => m);
                }
            }
            finally
            {
                if (e != null) ((IDisposable)e).Dispose();
            }

            IEnumerator<Func<int>> e1 = ((IEnumerable<Func<int>>)funcs).GetEnumerator();
            try
            {
                Func<int> m;
                while (e1.MoveNext())
                {
                    m = (Func<int>)e1.Current;
                    Console.WriteLine(m());
                    // prints 120 120 120
                }
            }
            finally
            {
                if (e1 != null) ((IDisposable)e1).Dispose();
            }
        }
    }
}
