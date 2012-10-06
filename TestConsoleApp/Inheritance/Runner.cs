using System;

namespace TestConsoleApp.Inheritance
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            new Derived();
        }
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

class Base
{
    readonly Foo baseFoo = new Foo("Base initializer");
    public Base()
    {
        Console.WriteLine("Base constructor");
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

class NoDefaultConstructor
{
    protected int i;

    public NoDefaultConstructor(int number)
    {
        i = number;
    }
}

class AnotherDerived : NoDefaultConstructor
{
    // must call the base constructor
    public AnotherDerived()
        : base(10)
    {

    }
}