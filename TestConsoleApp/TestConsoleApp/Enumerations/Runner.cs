using System;
using System.Linq;

namespace TestConsoleApp.Enumerations
{
    internal enum Test
    {
        First = 0,
        Second = 1,
        Third = 2,
        Unknown = 99
    }

    internal enum Another
    {
        First = 0,
        Second = 1,
        Third = 1,
        Fourth = 5
    }

    class Runner : IRunner
    {
        public void RunProgram()
        {
            Test();
            Another();
        }

        private static void Another()
        {
            foreach (var item in Enum.GetValues(typeof(Another)))
            {
                Console.WriteLine("{0}, {1}", item.ToString(), (int)item);
            }
        }

        private static void Test()
        {
            var range = Enumerable.Range(0, 5);

            foreach (int i in range)
            {
                if (Enum.IsDefined(typeof(Test), i))
                {
                    Console.WriteLine("i is " + i.ToString() + " and " + ((Test)i).ToString());
                }
                else
                {
                    Console.WriteLine("i is " + i.ToString() + " and not a member of Test.");
                }
            }
        }
    }
}
