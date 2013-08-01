using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.Permuatations
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            foreach (var permutation in Permuation.GetPermutations(4))
            {
                Console.WriteLine(permutation);
            }
        }

        private struct Permuation : IEnumerable<int>
        {
            private int[] permutation;

            private static Permuation empty = new Permuation(new int[] { });

            public Permuation(int[] input)
            {
                permutation = input;
            }

            private Permuation(IEnumerable<int> input)
                : this(input.ToArray())
            {
            }

            public static Permuation Empty
            {
                get { return empty; }
            }

            public int this[int index]
            {
                get { return permutation[index]; }
            }

            public int Count
            {
                get { return permutation.Length; }
            }

            public static IEnumerable<Permuation> GetPermutations(int count)
            {
                if (count < 0)
                {
                    throw new ArgumentOutOfRangeException("count");
                }

                return GetPermutationsIterator(count);
            }

            public IEnumerator<int> GetEnumerator()
            {
                foreach (var i in permutation)
                {
                    yield return i;
                }
            }

            public override string ToString()
            {
                return string.Join(",", permutation);
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private static IEnumerable<Permuation> GetPermutationsIterator(int count)
            {
                if (count == 0)
                {
                    yield return Empty;
                    yield break;
                }

                bool forwards = false;
                foreach (Permuation permuation in GetPermutationsIterator(count - 1))
                {
                    for (int index = 0; index < count; index++)
                    {
                        yield return new Permuation(permuation.InsertAt(forwards ? index : count - index - 1, count - 1));
                    }

                    forwards = !forwards;
                }
            }
        }
    }

    public static class Extensions
    {
        public static IEnumerable<T> InsertAt<T>(this IEnumerable<T> items, int position, T newItem)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (position < 0)
            {
                throw new ArgumentOutOfRangeException("position");
            }

            return InsertAtIterator<T>(items, position, newItem);
        }

        private static IEnumerable<T> InsertAtIterator<T>(this IEnumerable<T> items, int position, T newItem)
        {
            int index = 0;
            foreach (var item in items)
            {
                if (index == position)
                {
                    yield return newItem;
                }

                yield return item;
                index++;
            }

            if (index == position)
            {
                yield return newItem;
            }
        }
    }
}
