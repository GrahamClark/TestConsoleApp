using System.Diagnostics;

namespace TestConsoleApp.ConditionalCompilation
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            System.Console.WriteLine("hello");
            
            // this call is removed by the semantic analysis phase of compilation in Release builds,
            // as the target method has a Conditional("DEBUG") attribute.
            DebugMethod();
#if DEBUG
            // this call is removed by the lexical analysis phase of compilation in Release builds.
            AnotherDebugMethod();
#endif
        }

        [Conditional("DEBUG")]
        static void DebugMethod()
        {
            System.Console.WriteLine("there");
        }

        static void AnotherDebugMethod()
        {
            System.Console.WriteLine("again");
        }
    }
}
