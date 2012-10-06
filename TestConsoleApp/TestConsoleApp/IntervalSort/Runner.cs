using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TestConsoleApp.IntervalSort
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            char[] input = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q' };
            var sorted = SortArray(input);
            foreach (var item in sorted)
            {
                Console.Write("{0} ", item);
            }
        }

        // from http://stackoverflow.com/questions/8220113/different-sorting-orders-divide-and-conquer
        private IEnumerable<T> SortArray<T>(IEnumerable<T> input)
        {
            var length = input.Count();
            var array = Enumerable.Range(1, length + 1).ToArray();

            var first = input.First();
            var last = input.Last();

            yield return first;
            if (!last.Equals(first))
            {
                yield return last;
            }

            var intervals = new Queue<Interval>();
            intervals.Enqueue(new Interval(0, length));
            Interval currentItem;
            while (intervals.Count > 0)
            {
                currentItem = intervals.Dequeue();
                var interval = currentItem.leastUpperBound - currentItem.greatestLowerBound;
                if (interval > 1)
                {
                    var middle = (interval / 2) + currentItem.greatestLowerBound;
                    yield return input.ElementAt(middle);

                    intervals.Enqueue(new Interval(currentItem.greatestLowerBound, middle));
                    intervals.Enqueue(new Interval(middle, currentItem.leastUpperBound));
                }
            }
        }

        [DebuggerDisplay("{greatestLowerBound}-{leastUpperBound}")]
        private struct Interval
        {
            public Interval(int lower, int upper)
            {
                this.greatestLowerBound = lower;
                this.leastUpperBound = upper;
            }

            public readonly int greatestLowerBound;
            public readonly int leastUpperBound;
        }
    }
}
