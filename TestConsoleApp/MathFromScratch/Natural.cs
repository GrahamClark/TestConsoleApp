using System;
using System.Diagnostics;

namespace TestConsoleApp.MathFromScratch
{
    public sealed class Natural : IEquatable<Natural>, IComparable<Natural>
    {
        public static readonly Natural One = new Natural(Zero, OneBit);

        public static readonly Natural Zero = new Natural(null, ZeroBit);

        private static readonly Bit OneBit = new Bit();

        private static readonly Bit ZeroBit = new Bit();

        private readonly Bit head;

        private readonly Natural tail;

        private Natural(Natural tail, Bit head)
        {
            this.head = head;
            this.tail = tail;
        }

        public static Natural operator +(Natural x, Natural y)
        {
            if (ReferenceEquals(x, null))
            {
                throw new ArgumentNullException("x");
            }

            if (ReferenceEquals(y, null))
            {
                throw new ArgumentNullException("y");
            }

            return Add(x, y);
        }

        public static Natural operator -(Natural x, Natural y)
        {
            if (ReferenceEquals(x, null))
            {
                throw new ArgumentNullException("x");
            }

            if (ReferenceEquals(y, null))
            {
                throw new ArgumentNullException("y");
            }

            return Subtract(x, y);
        }

        public static Natural operator *(Natural x, Natural y)
        {
            if (ReferenceEquals(x, null))
            {
                throw new ArgumentNullException("x");
            }

            if (ReferenceEquals(y, null))
            {
                throw new ArgumentNullException("y");
            }

            return Multiply(x, y);
        }

        public static Natural operator /(Natural x, Natural y)
        {
            if (ReferenceEquals(x, null))
            {
                throw new ArgumentNullException("x");
            }

            if (ReferenceEquals(y, null))
            {
                throw new ArgumentNullException("y");
            }

            if (Equals(Zero, y))
            {
                throw new DivideByZeroException();
            }

            return DivideWithRemainder(x, y).Item1;
        }

        public static Natural operator %(Natural x, Natural y)
        {
            if (ReferenceEquals(x, null))
            {
                throw new ArgumentNullException("x");
            }

            if (ReferenceEquals(y, null))
            {
                throw new ArgumentNullException("y");
            }

            if (Equals(Zero, y))
            {
                throw new DivideByZeroException();
            }

            return DivideWithRemainder(x, y).Item2;
        }

        public static Natural operator ++(Natural x)
        {
            if (ReferenceEquals(x, null))
            {
                throw new ArgumentNullException("x");
            }

            return Add(x, One);
        }

        public static Natural operator --(Natural x)
        {
            if (ReferenceEquals(x, null))
            {
                throw new ArgumentNullException("x");
            }
            else if (ReferenceEquals(x, Zero))
            {
                throw new InvalidOperationException();
            }
            
            return Subtract(x, One);
        }

        public static bool operator <(Natural x, Natural y)
        {
            return CompareTo(x, y) < 0;
        }

        public static bool operator >(Natural x, Natural y)
        {
            return CompareTo(x, y) > 0;
        }

        public static bool operator <=(Natural x, Natural y)
        {
            return CompareTo(x, y) <= 0;
        }

        public static bool operator >=(Natural x, Natural y)
        {
            return CompareTo(x, y) >= 0;
        }

        public static bool operator ==(Natural x, Natural y)
        {
            return CompareTo(x, y) == 0;
        }

        public static bool operator !=(Natural x, Natural y)
        {
            return CompareTo(x, y) != 0;
        }

        public Natural Power(Natural exponent)
        {
            if (ReferenceEquals(exponent, null))
            {
                throw new ArgumentException("exponent");
            }

            return Power(this, exponent);
        }

        public override string ToString()
        {
            if (ReferenceEquals(this, Zero))
            {
                return "0";
            }

            return tail + head.ToString();
        }

        public override bool Equals(object obj)
        {
            return CompareTo(this, obj as Natural) == 0;
        }

        public bool Equals(Natural x)
        {
            return CompareTo(this, x) == 0;
        }

        public int CompareTo(Natural x)
        {
            return CompareTo(this, x);
        }

        public override int GetHashCode()
        {
            return ToInteger();
        }

        private static Natural Create(Natural tail, Bit head)
        {
            if (ReferenceEquals(tail, Zero))
            {
                return head == ZeroBit ? Zero : One;
            }

            return new Natural(tail, head);
        }

        private static Natural Add(Natural x, Natural y)
        {
            if (ReferenceEquals(x, Zero))
            {
                return y;
            }
            else if (ReferenceEquals(y, Zero))
            {
                return x;
            }
            else if (x.head == ZeroBit)
            {
                return Create(Add(x.tail, y.tail), y.head);
            }
            else if (y.head == ZeroBit)
            {
                return Create(Add(x.tail, y.tail), x.head);
            }
            else
            {
                return Create(Add(Add(x.tail, y.tail), One), ZeroBit);
            }
        }

        private static Natural Subtract(Natural x, Natural y)
        {
            if (ReferenceEquals(x, y))
            {
                return Zero;
            }
            else if (ReferenceEquals(y, Zero))
            {
                return x;
            }
            else if (ReferenceEquals(x, Zero))
            {
                throw new InvalidOperationException("Cannot subtract greater natural from lesser natural");
            }
            else if (x.head == y.head)
            {
                return Create(Subtract(x.tail, y.tail), ZeroBit);
            }
            else if (x.head == OneBit)
            {
                return Create(Subtract(x.tail, y.tail), OneBit);
            }
            else
            {
                return Create(Subtract(Subtract(x.tail, One), y.tail), OneBit);
            }
        }

        private static Natural Multiply(Natural x, Natural y)
        {
            if (ReferenceEquals(x, Zero))
            {
                return Zero;
            }
            else if (ReferenceEquals(y, Zero))
            {
                return Zero;
            }
            else if (ReferenceEquals(x, One))
            {
                return y;
            }
            else if (ReferenceEquals(y, One))
            {
                return x;
            }
            else if (x.head == ZeroBit)
            {
                return Create(Multiply(x.tail, y), ZeroBit);
            }
            else if (y.head == ZeroBit)
            {
                return Create(Multiply(x, y.tail), ZeroBit);
            }
            else
            {
                return Add(Create(Multiply(x, y.tail), ZeroBit), x);
            }
        }

        private static Natural Power(Natural x, Natural y)
        {
            if (ReferenceEquals(y, Zero))
            {
                return One;
            }
            else
            {
                var p = Power(x, y.tail);
                var result = Multiply(p, p);
                if (y.head == OneBit)
                {
                    result = Multiply(result, x);
                }

                return result;
            }
        }
        
        private static Tuple<Natural, Natural> DivideWithRemainder(Natural x, Natural y)
        {
            Debug.Assert(!ReferenceEquals(y, Zero));
            if (ReferenceEquals(x, Zero))
            {
                return Tuple.Create(Zero, Zero);
            }

            Tuple<Natural, Natural> tuple = DivideWithRemainder(x.tail, y);
            Natural quotient = Create(tuple.Item1, ZeroBit);
            Natural remainder = Create(tuple.Item2, x.head);
            if (remainder >= y)
            {
                remainder = Subtract(remainder, y);
                quotient = Add(quotient, One);
            }

            return Tuple.Create(quotient, remainder);
        }

        // negative means x < y 
        // positive means x > y 
        // zero means x == y 
        // two nulls are equal 
        // otherwise, null is always smaller 
        private static int CompareTo(Natural x, Natural y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }
            else if (ReferenceEquals(x, null))
            {
                return -1;
            }
            else if (ReferenceEquals(y, null))
            {
                return 1;
            }
            else if (ReferenceEquals(x, Zero))
            {
                return -1;
            }
            else if (ReferenceEquals(y, Zero))
            {
                return 1;
            }
            else if (x.head == y.head)
            {
                return CompareTo(x.tail, y.tail);
            }
            else if (x.head == ZeroBit)
            {
                return CompareTo(x.tail, y.tail) > 0 ? 1 : -1;
            }
            else
            {
                return CompareTo(x.tail, y.tail) < 0 ? -1 : 1;
            }
        }

        private int ToInteger()
        {
            if (ReferenceEquals(this, Zero))
            {
                return 0;
            }
            else
            {
                return (head == ZeroBit ? 0 : 1) + 2 * tail.ToInteger();
            }
        }

        private sealed class Bit
        {
            public override string ToString()
            {
                return this == ZeroBit ? "0" : "1";
            }
        }
    }
}
