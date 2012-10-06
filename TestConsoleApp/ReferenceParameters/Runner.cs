using System;
using System.Collections.Generic;

namespace TestConsoleApp.ReferenceParameters
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            List<string> l1 = new List<string>();
            AddByRef(ref l1);
            AddByRef(ref l1);
            AddByRef(ref l1);

            List<string> l2 = new List<string>();
            AddByVal(l2);
            AddByVal(l2);
            AddByVal(l2);

            Console.WriteLine("l1:");
            foreach(var s in l1)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("\nl2:");
            foreach(var s in l2)
            {
                Console.WriteLine(s);
            }
        }

        private void AddByRef(ref List<string> l)
        {
            l.Add("hello");
        }

        private void AddByVal(List<string> l)
        {
            l.Add("bye");
        }
    }
}
