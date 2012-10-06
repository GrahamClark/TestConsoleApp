using System;

namespace TestConsoleApp.Structs
{
    class Runner : IRunner
    {
        readonly MutableStruct ms = new MutableStruct();
        readonly MutableClass mc = new MutableClass();

        public void RunProgram()
        {
            Console.WriteLine("Struct:");
            Console.WriteLine(this.ms.Mutate());
            Console.WriteLine(this.ms.Mutate());
            Console.WriteLine(this.ms.Mutate());
            //as ms is readonly and structs are value types, this.ms gives a copy of the struct. 
            // This copy is mutated, but this.ms remains the same.

            Console.WriteLine("\nClass:");
            Console.WriteLine(this.mc.Mutate());
            Console.WriteLine(this.mc.Mutate());
            Console.WriteLine(this.mc.Mutate());
            //classes are reference types, so this.mc always returns the same instance.
        }
    }

    struct MutableStruct
    {
        private int x;
        public int Mutate()
        {
            this.x = this.x + 1;
            return this.x;
        }
    }

    class MutableClass
    {
        private int x;
        public int Mutate()
        {
            this.x = this.x + 1;
            return this.x;
        }
    }
}
