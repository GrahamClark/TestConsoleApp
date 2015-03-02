using System;

namespace TestConsoleApp.MathFromScratch
{
    public struct Integer : IComparable<Integer>, IEquatable<Integer>
    {
        private static readonly Sign Positive = new Sign();
        private static readonly Sign Negative = new Sign();

        private readonly Sign _sign;
        private readonly Natural _magnitude;

        public static readonly Integer Zero = new Integer(Positive, Natural.Zero);
        public static readonly Integer One = new Integer(Positive, Natural.One);

        private bool IsDefault { get { return _sign == null; } }

        private Integer(Sign sign, Natural magnitude)
        {
            _sign = sign;
            _magnitude = magnitude;
        }

        private static Integer Create(Sign sign, Natural magnitude)
        {
            if (magnitude == Natural.Zero)
            {
                return Zero;
            }
            else
            {
                return new Integer(sign, magnitude);
            }
        }

        public static explicit operator Natural(Integer x)
        {
            if (x.IsDefault) x = Zero;
            if (x._sign == Negative)
            {
                throw new InvalidCastException();
            }

            return x._magnitude;
        }

        public static explicit operator Integer(Natural x)
        {
            if (ReferenceEquals(x, null))
            {
                throw new ArgumentNullException("x");
            }

            return Create(Positive, x);
        }

        public int CompareTo(Integer other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Integer other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            Integer x = this;
            if (x.IsDefault) x = Zero;
            string str = x._magnitude.ToString();
            if (x._sign == Negative)
            {
                str = "-" + str;
            }

            return str;
        }

        private sealed class Sign
        {
            public override string ToString()
            {
                return this == Positive ? "+" : "-";
            }
        }
    }
}
