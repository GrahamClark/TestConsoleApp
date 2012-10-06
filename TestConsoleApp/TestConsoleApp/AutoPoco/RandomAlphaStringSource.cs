using System;
using System.Text;
using AutoPoco.Engine;

namespace TestConsoleApp.AutoPoco
{
    class RandomAlphaStringSource : DatasourceBase<string>
    {
        private static string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "z" };

        private readonly int minLength;
        private readonly int maxLength;
        private Random radom = new Random();

        public RandomAlphaStringSource()
        {
            this.minLength = 1;
            this.maxLength = 10;
        }

        public RandomAlphaStringSource(int min, int max)
        {
            this.minLength = min;
            this.maxLength = max;
        }

        public override string Next(IGenerationSession session)
        {
            int length = this.radom.Next(this.minLength, this.maxLength);

            StringBuilder builder = new StringBuilder(length);
            bool first = true;
            for (int i = 0; i < length; i++)
            {
                string character = letters[this.radom.Next(0, letters.Length - 1)];
                if (first)
                {
                    character = character.ToUpperInvariant();
                }

                builder.Append(character);
                first = false;
            }

            return builder.ToString();
        }
    }
}
