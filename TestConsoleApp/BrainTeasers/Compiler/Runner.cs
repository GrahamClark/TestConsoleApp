using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.BrainTeasers.Compiler
{
    /// <summary>
    /// Only the literal '0' should be convertible to an enum's default value.
    /// However, a compiler bug means the decimal 0.0 is also allowed.
    /// Early optimisation means that f2 is set to 0, and this also compiles.
    /// </summary>
    internal class Runner : IRunner
    {
        private const int One = 1;

        private const int Une = 1;

        private enum Foo
        {
            Bar,
            Baz
        }

        public void RunProgram()
        {
            Foo f1 = 0.0;
            Console.WriteLine(f1);

            Foo f2 = One - Une;
            Console.WriteLine(f2);
        }
    }
}
