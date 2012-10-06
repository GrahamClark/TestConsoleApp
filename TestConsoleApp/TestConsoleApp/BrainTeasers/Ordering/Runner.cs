using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.BrainTeasers.Ordering
{
    internal class Foo
    {
        static Foo()
        {
            Console.WriteLine("Foo");
        }
    }

    internal class Bar
    {
        private static int i = Init();

        public static int Init()
        {
            Console.WriteLine("Bar");
            return 0;
        }
    }

    /// <summary>
    /// In .Net 4, just "Foo" will be printed. In earlier versions, it usually prints "Bar" then "Foo" -
    /// this is not guaranteed however.
    /// </summary>
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            Foo f = new Foo();
            Bar b = new Bar();
        }
    }
}
