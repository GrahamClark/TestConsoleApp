using System;

namespace TestConsoleApp.RelativeMonthYear
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            MonthYear d1 = new MonthYear() { Month = 1, Year = 2012 };
            Console.WriteLine(ValidRelativeMonthYear(d1, 0, MonthYearUnit.Month, RangeBoundaryType.Inclusive));
            Console.WriteLine(ValidRelativeMonthYear(d1, 0, MonthYearUnit.Month, RangeBoundaryType.Exclusive));
            Console.WriteLine(ValidRelativeMonthYear(d1, 0, MonthYearUnit.Year, RangeBoundaryType.Inclusive));
            Console.WriteLine(ValidRelativeMonthYear(d1, 0, MonthYearUnit.Year, RangeBoundaryType.Exclusive));

            Console.WriteLine(ValidRelativeMonthYear(d1, 2, MonthYearUnit.Month, RangeBoundaryType.Inclusive,
                                                         3, MonthYearUnit.Year, RangeBoundaryType.Exclusive));
        }

        internal static bool ValidRelativeMonthYear(MonthYear input, int upperBound, MonthYearUnit upperUnit,
                                                    RangeBoundaryType upperBoundType)
        {
            return ValidRelativeMonthYear(input, 0, MonthYearUnit.Year, RangeBoundaryType.Ignore, upperBound, upperUnit, upperBoundType);
        }

        internal static bool ValidRelativeMonthYear(MonthYear input,
                                                    int lowerBound, MonthYearUnit lowerUnit, RangeBoundaryType lowerBoundType,
                                                    int upperBound, MonthYearUnit upperUnit, RangeBoundaryType upperBoundType)
        {
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lowerDate = DateTime.MinValue;
            DateTime upperDate = DateTime.MaxValue;
            DateTime inputDate = new DateTime(input.Year, input.Month, 1);

            switch (lowerUnit)
            {
                case MonthYearUnit.Month:
                    lowerDate = now.AddMonths(lowerBound * -1);
                    break;
                case MonthYearUnit.Year:
                    lowerDate = now.AddYears(lowerBound * -1);
                    break;
            }
            switch (upperUnit)
            {
                case MonthYearUnit.Month:
                    upperDate = now.AddMonths(upperBound);
                    break;
                case MonthYearUnit.Year:
                    upperDate = now.AddYears(upperBound);
                    break;
            }

            //default the bound check to true - if lowerBoundType is Ignore, no test will be performed.
            bool lowerBoundOk = true;
            if (lowerBoundType == RangeBoundaryType.Inclusive)
            {
                lowerBoundOk = inputDate.CompareTo(lowerDate) >= 0;
            }
            else if (lowerBoundType == RangeBoundaryType.Exclusive)
            {
                lowerBoundOk = inputDate.CompareTo(lowerDate) > 0;
            }

            //default the bound check to true - if upperBoundType is Ignore, no test will be performed.
            bool upperBoundOk = true;
            if (upperBoundType == RangeBoundaryType.Inclusive)
            {
                upperBoundOk = inputDate.CompareTo(upperDate) <= 0;
            }
            else if (upperBoundType == RangeBoundaryType.Exclusive)
            {
                upperBoundOk = inputDate.CompareTo(upperDate) < 0;
            }

            return lowerBoundOk && upperBoundOk;
        }
    }
}
