using System;

namespace TestConsoleApp.FileResources
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            Console.WriteLine(FileResources.Test);
        }
    }
}
