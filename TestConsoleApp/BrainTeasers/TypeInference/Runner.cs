using System;

namespace TestConsoleApp.BrainTeasers.TypeInference
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            string s = "hello";
            Foo(s); // prints params T[]
            // T is inferred to be System.String
            // choice between object and params string[]
            // params is expanded, so the choice is between object and string
            // string wins.
        }

        static void Foo(object x)
        {
            Console.WriteLine("object");
        }

        static void Foo<T>(params T[] x)
        {
            Console.WriteLine("params T[]");
        }
    }
}
