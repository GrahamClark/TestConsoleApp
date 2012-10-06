using System;
using System.Globalization;

namespace TestConsoleApp.Dates
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            DateDifferences();
            Console.WriteLine();
            WeekNumbers(CultureInfo.GetCultureInfo("en-GB"));
            Console.WriteLine();
            WeekNumbers(CultureInfo.GetCultureInfo("da-DK"));
        }

        private static void DateDifferences()
        {
            DateTime date1 = new DateTime(2000, 8, 1);
            DateTime date2 = new DateTime(2000, 12, 1);

            Console.WriteLine("date1 = " + date1);
            Console.WriteLine("date2 = " + date2);

            Console.WriteLine("Months difference = " + date2.Subtract(date1).Days / 30);
            Console.WriteLine("Ticks from date1 - date2 = " + (date1.Subtract(date2)).Ticks);
            Console.WriteLine("Ticks from date2 - date1 = " + (date2.Subtract(date1)).Ticks);
        }

        private static void WeekNumbers(CultureInfo culture)
        {
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
            CalendarWeekRule weekRule = culture.DateTimeFormat.CalendarWeekRule;
            Console.WriteLine("first day of week: {0}", firstDayOfWeek);
            Console.WriteLine("week rule: {0}\n", weekRule);

            DateTime date = new DateTime(2012, 12, 30);
            for (int i = 0; i <= 10; i++)
            {
                DateTime currentDate = date.AddDays(i);
                Console.WriteLine(
                    "Date: {0} WeekNumber: {1}",
                    currentDate.ToShortDateString(),
                    culture.Calendar.GetWeekOfYear(currentDate, weekRule, firstDayOfWeek));
            }
        }
    }
}
