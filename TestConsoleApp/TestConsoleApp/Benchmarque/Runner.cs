using System.Collections.Generic;
using Benchmarque;

namespace TestConsoleApp.Benchmarque
{
    class Runner
    {
        /**
         * Compile in Release Mode.
         * Open Package Manager Console.
         * cd .\TestConsoleApp\bin\Release
         * Start-Benchmark .\TestConsoleApp.exe
         **/
    }

    public class NameAppendBenchmark : Benchmark<IAppendText>
    {
        private static readonly string[] Names = new[]
            {
                "Adam", "Betty", "Charles", "David",
                "Edward", "Frodo", "Gandalf", "Henry",
                "Ida", "John", "King", "Larry", "Morpheus",
                "Neo", "Peter", "Quinn", "Ralphie", "Samwise",
                "Trinity", "Umma", "Vincent", "Wanda"
            };

        public IEnumerable<int> Iterations
        {
            get { return new[] { 1000, 10000 }; }
        }

        public void WarmUp(IAppendText instance)
        {
            instance.Append(Names);
        }

        public void Run(IAppendText instance, int iterationCount)
        {
            for (int i = 0; i < iterationCount; i++)
            {
                string result = instance.Append(Names);
            }
        }

        public void Shutdown(IAppendText instance)
        {
        }
    }
}
