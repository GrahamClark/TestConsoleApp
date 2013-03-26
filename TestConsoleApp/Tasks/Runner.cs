using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestConsoleApp.Tasks
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var input = "abc123efd";
            FirstTest(input);
            SecondTest(input);
        }

        private void FirstTest(string input)
        {
            var first = new Task<string>(() => GetNumbers(input));
            var second = first.ContinueWith(t => GetTotal(t.Result));
            var third = second.ContinueWith(t => GetPercentage(t.Result));

            first.Start();
            Console.WriteLine(third.Result);
        }

        private void SecondTest(string input)
        {
        }

        private string GetNumbers(string input)
        {
            return Regex.Match(input, "[0-9]+").Value;
        }

        private int GetTotal(string numbers)
        {
            return numbers.Sum(number => int.Parse(number.ToString()));
        }

        private double GetPercentage(int number)
        {
            return (double)number / 100;
        }
    }
}
