using System;

namespace TestConsoleApp.Currying
{
    class Runner : IRunner
    {
        static string SampleFunction(int a, int b, int c)
        {
            return String.Format("a={0}, b={1}, c={2}", a, b, c);
        }

        public void RunProgram()
        {
            PartialFunctionApplication();
            CurryFunction();
        }

        private void CurryFunction()
        {
            Func<int, int, int, string> function = SampleFunction;

            // Call via currying 
            Func<int, Func<int, Func<int, string>>> f1 = Curry(function);
            Func<int, Func<int, string>> f2 = f1(1);
            Func<int, string> f3 = f2(2);
            string result = f3(3);
            Console.WriteLine(result);

            // Or to do make all the calls together... 
            var curried = Curry(function);
            result = curried(1)(2)(3);
            Console.WriteLine(result);
        }

        private static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>(
            Func<T1, T2, T3, TResult> function)
        {
            return a => b => c => function(a, b, c);
        }

        private void PartialFunctionApplication()
        {
            // use a delegate to the function
            Func<int, int, int, string> function = SampleFunction;
            string result = function(1, 2, 3);
            Console.WriteLine(result);

            // call via partial application
            Func<int, int, string> partial1 = ApplyPartial(function, 1);
            Func<int, string> partial2 = ApplyPartial(partial1, 2);
            Func<string> partial3 = ApplyPartial(partial2, 3);
            result = partial3();
            Console.WriteLine(result);
        }

        private Func<T2, T3, TResult> ApplyPartial<T1, T2, T3, TResult>(
            Func<T1, T2, T3, TResult> function,
            T1 arg1)
        {
            return (b, c) => function(arg1, b, c);
        }

        private Func<T2, TResult> ApplyPartial<T1, T2, TResult>(
            Func<T1, T2, TResult> function,
            T1 arg1)
        {
            return b => function(arg1, b);
        }

        private Func<TResult> ApplyPartial<T1, TResult>(
            Func<T1, TResult> function,
            T1 arg1)
        {
            return () => function(arg1);
        }
    }
}
