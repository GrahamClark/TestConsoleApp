using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace TestConsoleApp.TimerScheduling
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            const int maxToProcessInPeriod = 3;
            const int periodInMilliseconds = 10000;

            var numbers = new[] { 1, 2, 4, 6, 72, 23, 49 }.ToList();
            var stopwatch = Stopwatch.StartNew();
            int processedInPeriod = 0;
            while (numbers.Count > 0)
            {
                Console.WriteLine(numbers[0]);
                numbers.RemoveAt(0);
                processedInPeriod++;

                var elapsed = stopwatch.ElapsedMilliseconds;
                if (processedInPeriod == maxToProcessInPeriod && elapsed < periodInMilliseconds)
                {
                    Console.WriteLine("sleeping for {0}ms", periodInMilliseconds - (int)elapsed);
                    Thread.Sleep(periodInMilliseconds - (int)elapsed);
                    processedInPeriod = 0;
                    stopwatch.Restart();
                }
            }

        }
    }
}
