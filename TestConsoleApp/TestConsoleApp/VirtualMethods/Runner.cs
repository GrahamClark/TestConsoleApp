using System;

namespace TestConsoleApp.VirtualMethods
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            Base test = new Child();
            Console.WriteLine(test.Main());
            
            Console.WriteLine();
            Base test2 = new AnotherChild();
            Console.WriteLine(test2.Main());
        }
    }
}
