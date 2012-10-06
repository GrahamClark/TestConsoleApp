using System;

namespace TestConsoleApp.ExplicitInterface
{
    interface ITest
    {
        void Print(string message);
    }

    class Test : ITest
    {
        void ITest.Print(string message)
        {
            Console.WriteLine(message);
        }
    }

    class Runner : IRunner
    {
        public void RunProgram()
        {
            // no Print method on Test objects
            Test test1 = new Test();

            ITest test2 = new Test();
            test2.Print("hello");
        }
    }
}
