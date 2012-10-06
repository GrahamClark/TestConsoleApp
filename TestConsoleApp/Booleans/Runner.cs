using System;

namespace TestConsoleApp.Booleans
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            bool a = true;
            bool b = true;
            bool c = false;
            bool d = false;

            Console.WriteLine(String.Format("a = {0}\nb = {1}\nc = {2}\nd = {3}", a, b, c, d));
            Console.WriteLine("a xor b = " + (a ^ b));
            Console.WriteLine("b xor c = " + (b ^ c));
            Console.WriteLine("c xor a = " + (c ^ a));
            Console.WriteLine("c xor d = " + (c ^ d));
        }
    }
}
