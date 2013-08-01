using System;

namespace TestConsoleApp.GenericTypeConstraints
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            Print(new Concrete1());
            Print(new Concrete2());
        }

        public void Print<T>(T t)
            where T : Base
        {
            t.PrintName();
        }
    }

    abstract class Base
    {
        public virtual void PrintName()
        {
            Console.WriteLine("Base");
        }
    }

    class Concrete1 : Base
    {
        public override void PrintName()
        {
            Console.WriteLine("Concrete1");
        }
    }

    class Concrete2 : Base
    {
        public override void PrintName()
        {
            Console.WriteLine("Concrete2");
        }
    }
}
