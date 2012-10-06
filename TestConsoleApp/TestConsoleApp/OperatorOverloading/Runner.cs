using System;

namespace TestConsoleApp.OperatorOverloading
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            C ctrue = new C("true");
            C cfalse = new C("false");
            C cfrob = new C("frob");

            Console.WriteLine(ctrue && cfrob);
            Console.WriteLine(cfalse && cfrob);
            Console.WriteLine(cfrob && cfrob);
        }

        class C
        {
            string s;

            public C(string s)
            {
                this.s = s;
            }

            public override string ToString()
            {
                return this.s;
            }

            public static C operator &(C x, C y)
            {
                return new C(x.s + " & " + y.s);
            }

            public static C operator |(C x, C y)
            {
                return new C(x.s + " | " + y.s);
            }

            public static bool operator true(C x)
            {
                return x.s == "true";
            }

            public static bool operator false(C x)
            {
                return x.s == "false";
            }
        }
    }
}
