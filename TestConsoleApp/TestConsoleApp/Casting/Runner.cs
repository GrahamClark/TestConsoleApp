using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.Casting
{
    /// <summary>
    /// From http://blogs.msdn.com/b/ericlippert/archive/2012/07/10/when-is-a-cast-not-a-cast.aspx
    /// </summary>
    class Runner : IRunner
    {
        public void RunProgram()
        {
            List<string> l = new List<string> { "a", "b" };
            var s = l.Select(a => new { S = a }).First();
            var o = (object)s;
        }

        class C<T> { }

        class D
        {
            public static C<U> M1<U>(C<bool> c)
            {
                //return (C<U>)c;  <- compiler error: will only work if U is bool, and it might not be.

                // no compile-time error: an object is required for the Cast method, and an object
                // is supplied. The method will return an object of the same type as the generic
                // type parameter (C<U>), so this looks fine to. At runtime, if U is not bool, the
                // Cast method will throw an exception as there is no conversion from C<bool> to C<U>.
                return X.Cast<C<U>>(c);
            }

            public static C<U> M2<U>(C<bool> c)
            {
                // an inlined version of M1. Because of the initial cast to object, 
                return (C<U>)(object)c;
            }
        }

        public static class X
        {
            public static V Cast<V>(object obj)
            {
                return (V)obj;
            }
        }
    }
}
