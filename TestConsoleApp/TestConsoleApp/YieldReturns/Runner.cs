using System;
using System.Collections.Generic;

namespace TestConsoleApp.YieldReturns
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            Test1();
            Test2();
        }

        public void Test1()
        {
            foreach (var name in this.GetNames())
            {
                Console.WriteLine(name);
            }
        }

        public void Test2()
        {
            bool gotException = false;

            try
            {
                this.GetStrings(null);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("got expected exception");
                gotException = true;
            }
            catch (Exception)
            {
                Console.WriteLine("got unexpected exception");
                gotException = true;
            }
            finally
            {
                if (!gotException)
                {
                    Console.WriteLine("no exception thrown");
                }
            }
        }

        private IEnumerable<string> GetNames()
        {
            yield return "Bob";
            yield return "Bill";
            yield return "Alice";
            yield return "Stacy";
            yield return "Joe";
        }

        private IEnumerable<string> GetStrings(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            yield return s;
        }
    }
}
