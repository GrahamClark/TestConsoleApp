using System;

namespace TestConsoleApp.NestedGenerics
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            var o = new A<int>.B<char>.C<bool>();

            Console.WriteLine(o.a.GetType());
            Console.WriteLine(o.b.GetType());
            Console.WriteLine(o.c.GetType());
        }
    }

    public class A<T1>
    {
        public T1 a;

        public class B<T2> : A<T2>
        {
            public T1 b;

            public class C<T3> : B<T3>
            {
                public T1 c;
            }
        }
    }

    /*
    public class A<AT1> { 
        public AT1 a; 
    
        public class B<BT1,BT2> : A<BT2> { 
            public BT1 b; 

            public class C<CT1,CT2,CT3> : B<???,CT3> { 
                public CT1 c; 
            } 
        } 
    } 
    */
}
