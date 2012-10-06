using System;

namespace TestConsoleApp.MethodResolution
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            M1();
            M1(null);
            M1((string)null);
            M1(null, null);
            M1("hello", "there");
            M1(new string[] { "hello", "there" });

            Console.WriteLine();
            M2(null);
        }

        void M1(params string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("M1 args is null");
            }
            else
            {
                Console.WriteLine("M1 args has length {0}", args.Length);
                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine("\tElement {0} has value {1}", i, args[i]);
                }
            }
        }

        void M2(object arg)
        {
            Console.WriteLine("M2 with single object parameter chosen");
        }

        void M2(params object[] args)
        {
            Console.WriteLine("M2 with params chosen");
            if (args == null)
            {
                Console.WriteLine("args is null");
            }
            else
            {
                Console.WriteLine("args has length {0}", args.Length);
            }
        }
    }
}
