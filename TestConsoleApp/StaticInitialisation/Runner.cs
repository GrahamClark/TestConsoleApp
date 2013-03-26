using System;

namespace TestConsoleApp.StaticInitialisation
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            ArgumentsTest();
            Console.WriteLine();
            ConstructorTest();
        }

        /// <summary>
        /// needs to be run in Release mode.
        /// </summary>
        private static void ArgumentsTest()
        {
            Console.WriteLine("with no args:");
            StaticConstructorTest.Test(new string[0]);

            Console.WriteLine("\nwith args:");
            StaticConstructorTest.Test(new[] { "hello" });
        }

        private static void ConstructorTest()
        {
            Console.WriteLine("Main");
            new D();
        }
    }

    /// <summary>
    /// With the static constructor in StaticConstructorType, both .Net 3.5 and 4.0 will
    /// write exactly one line.
    /// Without the constructor, .Net 3.5 will write "Type initialised" and "No args" if
    /// no args were present, and just "Type initialised" if there were.
    /// Without the constructor, .Net 4.0 will never print "Type initialised".
    /// </summary>
    internal class StaticConstructorTest
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

    internal class StaticConstructorType
    {
        private static int x = Log();

        //static StaticConstructorType()
        //{
        //}

        public static void StaticMethod()
        {
        }

        private static int Log()
        {
            Console.WriteLine("Type initialised");
            return 0;
        }
    }

    internal class B
    {
        static B()
        {
            Console.WriteLine("B cctor");
        }

        public B()
        {
            Console.WriteLine("B ctor");
        }

        public static void M()
        {
            Console.WriteLine("B.M");
        }
    }

    internal class D : B
    {
        static D()
        {
            Console.WriteLine("D cctor");
        }

        public D()
        {
            Console.WriteLine("D ctor");
        }

        public static void N()
        {
            Console.WriteLine("D.N");
        }
    }
}
