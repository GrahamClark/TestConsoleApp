using System;
using AutoPoco.Engine;

namespace TestConsoleApp.AutoPoco
{
    class RandomIntegerSource : DatasourceBase<int>
    {
        private Random random = new Random();
        private readonly int minValue;
        private readonly int maxValue;

        public RandomIntegerSource()
        {
            this.minValue = 1;
            this.maxValue = 100;
        }

        public RandomIntegerSource(int min, int max)
        {
            this.minValue = min;
            this.maxValue = max;
        }

        public override int Next(IGenerationSession session)
        {
            return this.random.Next(this.minValue, this.maxValue);
        }
    }
}
