using System.Collections.Generic;

namespace TestConsoleApp.PythonScripting
{
    public class SalesBasket
    {
        public List<Line> Lines { get; set; }

        public double Total
        {
            get
            {
                double total = 0;
                foreach (Line item in Lines)
                {
                    total += item.Amount;
                }

                return total;
            }
        }
    }
}
