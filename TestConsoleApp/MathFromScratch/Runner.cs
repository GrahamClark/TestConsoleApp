using System;

namespace TestConsoleApp.MathFromScratch
{
    public class Runner : IRunner
    {
        private readonly Natural n0 = Natural.Zero;
        private readonly Natural n1 = Natural.One;

        public void RunProgram()
        {
            Addition();
            Console.WriteLine();
            Multiplication();
        }

        private void Multiplication()
        {
            // 2 * 3 * 5 = 30 (011110)
            Console.WriteLine((n1 + n1) * (n1 + n1 + n1) * (n1 + n1 + n1 + n1 + n1));
        }

        private void Addition()
        {
            var n2 = n1 + n1;
            var n3 = n2 + n1;
            var n4 = n2 + n2;
            var n5 = n2 + n3;
            var n6 = n3 + n3;
            Console.WriteLine(n0);
            Console.WriteLine(n1);
            Console.WriteLine(n2);
            Console.WriteLine(n3);
            Console.WriteLine(n4);
            Console.WriteLine(n5);
            Console.WriteLine(n6);
        }
    }
}
