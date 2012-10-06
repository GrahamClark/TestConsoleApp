using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.BrainTeasers.Arithmetic
{
    internal class Runner : IRunner
    {
        /// <summary>
        /// doubles use binary floating point storage.
        /// </summary>
        public void RunProgram()
        {
            double d1 = 1.000001;
            double d2 = 0.000001;
            Console.WriteLine((d1 - d2) == 1.0); // will print false

            decimal c1 = 1.000001M;
            decimal c2 = 0.000001M;
            Console.WriteLine((c1 - c2) == 1.0M); // will print true

            double x = 1.11;
            double y = 0.89;
            Console.WriteLine(x + y);
            Console.WriteLine((x + y) == 2.0);
        }
    }
}
