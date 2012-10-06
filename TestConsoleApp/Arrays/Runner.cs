using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.Arrays
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            // rectangular array - 2-D
            int[,] rectangle = 
            {
                { 10, 20 },
                { 30, 40 },
                { 50, 60 }
            };

            // ragged array - array of arrays
            int[][] ragged = 
            {
                new[] { 10 },
                new[] { 20, 30 },
                new[] { 40, 50, 60 }
            };

            // array of 2-D arrays
            int[,][] crazy =
            {
                { new[] { 10 }, new[] { 15 } },
                { new[] { 20, 30 }, new[] { 25, 35 } },
                { new[] { 40, 50, 60 }, new[] { 45 } }
            };

            Console.WriteLine("rectangle.GetType().Name = {0}", rectangle.GetType().Name);
            Console.WriteLine("rectangle.Length = {0}", rectangle.Length);
            Console.WriteLine("rectangle.GetLength(0) = {0}", rectangle.GetLength(0));
            Console.WriteLine("rectangle.GetLength(1) = {0}", rectangle.GetLength(1));
            Console.WriteLine("rectangle[1,1] = {0}", rectangle[1, 1]);
            Console.WriteLine();

            Console.WriteLine("ragged.GetType().Name = {0}", ragged.GetType().Name);
            Console.WriteLine("ragged.Length = {0}", ragged.Length);
            Console.WriteLine("ragged.GetLength(0) = {0}", ragged.GetLength(0));
            Console.WriteLine("ragged[0].Length = {0}", ragged[0].Length);
            Console.WriteLine("ragged[1].Length = {0}", ragged[1].Length);
            Console.WriteLine("ragged[2].Length = {0}", ragged[2].Length);
            Console.WriteLine("ragged[2][0] = {0}", ragged[2][0]);
            Console.WriteLine();

            Console.WriteLine("crazy.GetType().Name = {0}", crazy.GetType().Name);
            Console.WriteLine("crazy.Length = {0}", crazy.Length);
            Console.WriteLine("crazy.GetLength(0) = {0}", crazy.GetLength(0));
            Console.WriteLine("crazy.GetLength(1) = {0}", crazy.GetLength(1));
            Console.WriteLine("crazy[2,0][0] = {0}", crazy[2, 0][0]);
            Console.WriteLine("crazy[1,1][0] = {0}", crazy[1, 1][0]);
        }
    }
}
