using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.RandomNumberDistributions
{
    /// <summary>
    /// from http://blogs.msdn.com/b/ericlippert/archive/2012/02/21/generating-random-non-uniform-data-in-c.aspx
    /// </summary>
    class Runner : IRunner
    {
        private static Random random = new Random();

        public void RunProgram()
        {
            var cauchy = from x in UniformDistribution() select CauchyQuantile(x);
            int[] histogram = CreateHistogram(cauchy.Take(100000), 50, -5.0, 5.0);
        }

        private static int[] CreateHistogram(IEnumerable<double> data, int buckets, double min, double max)
        {
            int[] results = new int[buckets];
            double multiplier = buckets / (max - min);

            foreach (double datum in data)
            {
                double index = (datum - min) * multiplier;
                if (0.0 <= index && index < buckets)
                {
                    results[(int)index] += 1;
                }
            }

            return results;
        }

        private static double CauchyQuantile(double p)
        {
            return Math.Tan(Math.PI * (p - 0.5));
        }

        private static IEnumerable<double> UniformDistribution()
        {
            while (true)
            {
                yield return random.NextDouble();
            }
        }
    }
}
