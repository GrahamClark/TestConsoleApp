using System;

namespace TestConsoleApp.AgeAttained
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }
                else
                {
                    try
                    {
                        DateTime dob = DateTime.Parse(input);
                        int years;
                        int months;
                        int quarters;

                        DateTime now = DateTime.Today;
                        years = now.Year - dob.Year;
                        months = now.Month - dob.Month;

                        bool sameMonthFutureDay = now.Month == dob.Month && now.Day < dob.Day;

                        if (now.Month < dob.Month || sameMonthFutureDay)
                        {
                            --years;
                        }

                        if (sameMonthFutureDay)
                        {
                            --months;
                        }

                        if (months >= 0 && months < 3)
                        {
                            quarters = 0;
                        }
                        else if (months >= 3 && months < 6)
                        {
                            quarters = 1;
                        }
                        else if (months >= 6 && months < 9)
                        {
                            quarters = 2;
                        }
                        else
                        {
                            quarters = 3;
                        }

                        Console.WriteLine("years = " + years);
                        Console.WriteLine("quarters = " + quarters);
                        Console.WriteLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
