using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.Dates
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            DateTime date1 = new DateTime(2000, 8, 1);
            DateTime date2 = new DateTime(2000, 12, 1);

            Console.WriteLine("date1 = " + date1);
            Console.WriteLine("date2 = " + date2);

            Console.WriteLine("Months difference = " + date2.Subtract(date1).Days/30);
            Console.WriteLine("Ticks from date1 - date2 = " + (date1.Subtract(date2)).Ticks);
            Console.WriteLine("Ticks from date2 - date1 = " + (date2.Subtract(date1)).Ticks);
        }
    }
}
