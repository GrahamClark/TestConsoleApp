using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace TestConsoleApp.Threading
{
    internal class Runner : IRunner
    {
        public delegate string StringWorker(string input);
        public static Random random = new Random();

        public void RunProgram()
        {
            //QueueWorkItem();
            AsyncDelegate();
        }

        private void AsyncDelegate()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            StringWorker worker = new StringWorker(StringMangler);
            List<IAsyncResult> results = new List<IAsyncResult>();
            for (int i = 0; i < 10; i++)
            {
                results.Add(worker.BeginInvoke("hello" + i.ToString(), null, null));
            }

            List<string> outputs = new List<string>();
            foreach (var result in results)
            {
                outputs.Add(worker.EndInvoke(result));
            }

            sw.Stop();
            Console.WriteLine("Work Time = " + sw.ElapsedMilliseconds.ToString());

            foreach (var item in outputs)
            {
                Console.WriteLine(item);
            }
        }

        public string StringMangler(string input)
        {
            int sleep = random.Next() % 2000;
            Thread.Sleep(sleep);
            return new string(input.Reverse().ToArray()) + " " + sleep.ToString();
        }

        private void QueueWorkItem()
        {
            int ctr = 0;
            for (int i = 0; i < 20; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Worker), new Tuple<int, int>(i, ++ctr));
            }

            Thread.Sleep(1000);
        }

        public void Worker(object state)
        {
            var data = (Tuple<int, int>)state;
            Console.WriteLine("Thread {0}, counter = {1}", data.Item1.ToString(), data.Item2.ToString());
        }
    }
}
