using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp.InitializationOrder
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            new Derived();
        }
    }

    class Foo
    {
        public Foo(string s)
        {
            Console.WriteLine("Foo constructor: {0}", s);
        }

        public void Bar() { }
    }

    class Base
    {
        readonly Foo baseFoo = new Foo("Base initializer");

        public Base()
        {
            Console.WriteLine("Base constructor");
        }
    }

    class Derived : Base
    {
        readonly Foo derivedFoo = new Foo("Derived initializer");

        public Derived()
        {
            Console.WriteLine("Derived constructor");
        }
    }
}
