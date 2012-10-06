using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsoleApp.Properties;

namespace TestConsoleApp.BinaryToBase64
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            Console.WriteLine(Convert.ToBase64String(Resources.test));
        }
    }
}
