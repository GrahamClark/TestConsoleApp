using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.BrainTeasers.Delegates
{
    internal class Runner : IRunner
    {
        internal delegate void Printer();

        /// <summary>
        /// There is just one variable 'i', so this is used by the delegate. At the time of the
        /// foreach loop, i is 10, so a list of '10's will be printed out.
        /// In order for a list of ascending numbers to be printed, the delegate can be given
        /// a variable that is declared inside the loop.
        /// </summary>
        public void RunProgram()
        {
            List<Printer> printers = new List<Printer>();
            for (int i = 0; i < 10; i++)
            {
                printers.Add(delegate { Console.WriteLine(i); });
            }

            foreach (var printer in printers)
            {
                printer();
            }
        }
    }
}
