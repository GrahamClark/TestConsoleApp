using System.Collections.Generic;

namespace TestConsoleApp.Comparing
{
    class AssumedBonusRateComparer : IComparer<QuoteResult>
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
            int bonusRateCompareValue;
            if(x.AssumedBonusRate.HasValue && y.AssumedBonusRate.HasValue)
            {
                bonusRateCompareValue = (this.Direction == SortDirection.Descending) ?
                    (int)(y.AssumedBonusRate - x.AssumedBonusRate) : (int)(x.AssumedBonusRate - y.AssumedBonusRate);
            }
            else if(x.AssumedBonusRate.HasValue && !y.AssumedBonusRate.HasValue)
            {
                bonusRateCompareValue = - 1;
            }
            else if(!x.AssumedBonusRate.HasValue && y.AssumedBonusRate.HasValue)
            {
                bonusRateCompareValue = 1;
            }
            else
            {
                bonusRateCompareValue = 0;
            }

            if(UseSubComparer && bonusRateCompareValue == 0)
            {
                var annuityComparer = new AnnualAnnuityComparer { Direction = this.Direction, UseSubComparer = false };
                bonusRateCompareValue = annuityComparer.Compare(x, y);
            }

            return bonusRateCompareValue;
        }
    }
}
