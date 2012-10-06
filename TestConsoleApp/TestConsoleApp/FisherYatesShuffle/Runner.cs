using System;

namespace TestConsoleApp.FisherYatesShuffle
{
    class Runner : IRunner
    {
        private static Random random = new Random();

        public void RunProgram()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine(String.Join(",", array));
            Shuffle(array);
            Console.WriteLine(String.Join(",", array));
        }

        static void Shuffle<T>(T[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i);
                T tmp = array[i];
                array[i] = array[j];
                array[j] = tmp;
            }
        }
    }
}
