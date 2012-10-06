using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.StaticInitialisation
{
    /// <summary>
    /// needs to be run in Release mode.
    /// </summary>
    class Runner : IRunner
    {
        public void RunProgram()
        {
            Console.WriteLine("with no args:");
            StaticConstructorTest.Test(new string[0]);

            Console.WriteLine("\nwith args:");
            StaticConstructorTest.Test(new string[] { "hello" });
        }
    }

    /// <summary>
    /// With the static constructor in StaticConstructorType, both .Net 3.5 and 4.0 will
    /// write exactly one line.
    /// Without the constructor, .Net 3.5 will write "Type initialised" and "No args" if
    /// no args were present, and just "Type initialised" if there were.
    /// Without the constructor, .Net 4.0 will never print "Type initialised".
    /// </summary>
    class StaticConstructorTest
    {
        public static void Test(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No args");
            }
            else
            {
                StaticConstructorType.StaticMethod();
            }
        }
	}

    class StaticConstructorType
    {
        private static int x = Log();

        //static StaticConstructorType()
        //{
        //}

        private static int Log()
        {
            Console.WriteLine("Type initialised");
            return 0;
        }

        public static void StaticMethod()
        {
        }
    }
}
