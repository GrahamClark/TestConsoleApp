using System;

namespace TestConsoleApp.Inheritance
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var d1 = new Derived();
            Derived.number = 20;

            var d2 = new D();
            Console.WriteLine(D.number); //20
            Base.number = 30;

            Console.WriteLine(Derived.number); //30
            Console.WriteLine(D.number); //30
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

class D : Base
{
}

class Base
{
    readonly Foo baseFoo = new Foo("Base initializer");

    public static int number = 10;

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