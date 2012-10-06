using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.Comparing
{
    class QuoteResult
    {
        public int Id { get; set; }

        public float? AnnualAnnuity { get; set; }

        public float? AssumedBonusRate { get; set; }

        public override string ToString()
        {
            string ann = AnnualAnnuity.HasValue ? AnnualAnnuity.Value.ToString() : "n/a";
            string ass = AssumedBonusRate.HasValue ? AssumedBonusRate.Value.ToString() : "n/a";

            return string.Format("{0}:\t{1}\t{2}", Id, ann, ass);
        }
    }
}
