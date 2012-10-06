using System.Collections.Generic;

namespace TestConsoleApp.Comparing
{
    class AnnualAnnuityComparer : IComparer<QuoteResult>
    {
        private bool useSubComparer = true;

        public bool UseSubComparer
        {
            get { return useSubComparer; }
            set { useSubComparer = value; }
        }

        public SortDirection Direction { get; set; }

        public int Compare(QuoteResult x, QuoteResult y)
        {
            int annualAnnuityCompareValue;
            if(x.AnnualAnnuity.HasValue && y.AnnualAnnuity.HasValue)
            {
                float difference = (this.Direction == SortDirection.Descending) ?
                    y.AnnualAnnuity.Value - x.AnnualAnnuity.Value : x.AnnualAnnuity.Value - y.AnnualAnnuity.Value;
                if (difference < 0)
                {
                    annualAnnuityCompareValue = -1;
                }
                else if (difference > 0)
                {
                    annualAnnuityCompareValue = 1;
                }
                else
                {
                    annualAnnuityCompareValue = 0;
                }
            }
            else if(x.AnnualAnnuity.HasValue && !y.AnnualAnnuity.HasValue)
            {
                annualAnnuityCompareValue = - 1;
            }
            else if(!x.AnnualAnnuity.HasValue && y.AnnualAnnuity.HasValue)
            {
                annualAnnuityCompareValue = 1;
            }
            else
            {
                annualAnnuityCompareValue = 0;
            }

            if(UseSubComparer && annualAnnuityCompareValue == 0)
            {
                var bonusRateComparer = new AssumedBonusRateComparer() { Direction = this.Direction, UseSubComparer = false };
                annualAnnuityCompareValue = bonusRateComparer.Compare(x, y);
            }

            return annualAnnuityCompareValue;
        }
    }
}
