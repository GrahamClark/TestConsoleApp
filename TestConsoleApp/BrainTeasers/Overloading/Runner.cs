using System;

namespace TestConsoleApp.BrainTeasers.Overloading
{
    internal class Runner : IRunner
    {
        internal class Base
        {
            public virtual void Foo(int x)
            {
                Console.WriteLine("Base.Foo(int)");
            }
        }

        internal class Derived : Base
        {
            public override void Foo(int x)
            {
                Console.WriteLine("Derived.Foo(int)");
            }

            public void Foo(object o)
            {
                Console.WriteLine("Derived.Foo(object)");
            }
        }

        /// <summary>
        /// Derived.Foo(object) is printed - when choosing an overload, if there are any compatible methods 
        /// declared in a derived class, all signatures declared in the base class are ignored - even if 
        /// they're overridden in the same derived class!
        /// </summary>
        public void RunProgram()
        {
            Derived d = new Derived();
            int i = 10;
            d.Foo(i);
        }
    }
}
