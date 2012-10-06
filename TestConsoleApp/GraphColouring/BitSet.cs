using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestConsoleApp.GraphColouring
{
    /// <summary>
    /// An immutable set of integers from 0 to 31; a convenient wrapper around bit operations on an int.
    /// </summary>
    internal struct BitSet : IEnumerable<int>
    {
        private readonly int bits;

        private BitSet(int bits)
        {
            this.bits = bits;
        }

        public static BitSet Empty
        {
            get { return default(BitSet); }
        }

        public bool Contains(int item)
        {
            Debug.Assert(0 <= item && item <= 31);
            return (this.bits & (1 << item)) != 0;
        }

        public BitSet Add(int item)
        {
            Debug.Assert(0 <= item && item <= 31);
            return new BitSet(this.bits | (1 << item));
        }

        public BitSet Remove(int item)
        {
            Debug.Assert(0 <= item && item <= 31);
            return new BitSet(this.bits & ~(1 << item));
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int item = 0; item < 32; ++item)
            {
                if (this.Contains(item))
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
