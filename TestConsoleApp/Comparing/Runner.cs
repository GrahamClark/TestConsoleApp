using System;
using System.Collections.Generic;

namespace TestConsoleApp.Comparing
{
    class Runner : IRunner
    {
        List<QuoteResult> results;

        public void RunProgram()
        {
            //results = new List<QuoteResult>()
            //{
            //    new QuoteResult { Id = 1, AnnualAnnuity = 4000f, AssumedBonusRate = 2f },
            //    new QuoteResult { Id = 2, AnnualAnnuity = 3879f, AssumedBonusRate = 3f },
            //    new QuoteResult { Id = 3, AnnualAnnuity = null, AssumedBonusRate = null },
            //    new QuoteResult { Id = 4, AnnualAnnuity = 4823f, AssumedBonusRate = null },
            //    new QuoteResult { Id = 5, AnnualAnnuity = null, AssumedBonusRate = 0f },
            //    new QuoteResult { Id = 6, AnnualAnnuity = 2934f, AssumedBonusRate = 4f },
            //    new QuoteResult { Id = 7, AnnualAnnuity = 4000f, AssumedBonusRate = 3f },
            //    new QuoteResult { Id = 8, AnnualAnnuity = 3259f, AssumedBonusRate = 2f },
            //    new QuoteResult { Id = 9, AnnualAnnuity = 4100f, AssumedBonusRate = 3f }
            //};
            results = new List<QuoteResult>()
            {
                new QuoteResult { Id = 8, AnnualAnnuity = 1045.92f, AssumedBonusRate = null },
                new QuoteResult { Id = 4, AnnualAnnuity = 1046.64f, AssumedBonusRate = null },
                new QuoteResult { Id = 7, AnnualAnnuity = 1070f, AssumedBonusRate = null }
            };
            PrintResults(":");

            results.Sort(new AnnualAnnuityComparer { Direction = SortDirection.Descending });
            PrintResults("sorted by Annual Annuity descending:");

            results.Sort(new AnnualAnnuityComparer { Direction = SortDirection.Ascending });
            PrintResults("sorted by Annual Annuity ascending:");

            results.Sort(new AssumedBonusRateComparer { Direction = SortDirection.Descending });
            PrintResults("sorted by Assumed Bonus Rate descending");

            results.Sort(new AssumedBonusRateComparer { Direction = SortDirection.Ascending });
            PrintResults("sorted by Assumed Bonus Rate ascending");
        }

        private void PrintResults(string note)
        {
            Console.WriteLine("Results list " + note);
            foreach(var item in results)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}
