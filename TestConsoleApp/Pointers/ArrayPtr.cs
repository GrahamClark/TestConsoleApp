using System;
using System.Diagnostics;

namespace TestConsoleApp.Pointers
{
    internal struct ArrayPtr<T>
    {
        public static ArrayPtr<T> Null { get { return default(ArrayPtr<T>); } }
        private readonly T[] source;
        private readonly int index;

        private ArrayPtr(ArrayPtr<T> old, int delta)
        {
            this.source = old.source;
            this.index = old.index + delta;
            Debug.Assert(index >= 0);
            Debug.Assert(index == 0 || this.source != null && index < this.source.Length);
        }

        public ArrayPtr(T[] source)
        {
            this.source = source;
            index = 0;
        }

        public bool IsNull()
        {
            return this.source == null;
        }

        public static bool operator <(ArrayPtr<T> a, ArrayPtr<T> b)
        {
            Debug.Assert(Object.ReferenceEquals(a.source, b.source));
            return a.index < b.index;
        }

        public static bool operator >(ArrayPtr<T> a, ArrayPtr<T> b)
        {
            Debug.Assert(Object.ReferenceEquals(a.source, b.source));
            return a.index > b.index;
        }

        public static bool operator <=(ArrayPtr<T> a, ArrayPtr<T> b)
        {
            Debug.Assert(Object.ReferenceEquals(a.source, b.source));
            return a.index <= b.index;
        }

        public static bool operator >=(ArrayPtr<T> a, ArrayPtr<T> b)
        {
            Debug.Assert(Object.ReferenceEquals(a.source, b.source));
            return a.index >= b.index;
        }

        public static int operator -(ArrayPtr<T> a, ArrayPtr<T> b)
        {
            Debug.Assert(Object.ReferenceEquals(a.source, b.source));
            return a.index - b.index;
        }

        public static ArrayPtr<T> operator +(ArrayPtr<T> a, int count)
        {
            return new ArrayPtr<T>(a, +count);
        }

        public static ArrayPtr<T> operator -(ArrayPtr<T> a, int count)
        {
            return new ArrayPtr<T>(a, -count);
        }

        public static ArrayPtr<T> operator ++(ArrayPtr<T> a)
        {
            return a + 1;
        }

        public static ArrayPtr<T> operator --(ArrayPtr<T> a)
        {
            return a - 1;
        }

        public static implicit operator ArrayPtr<T>(T[] x)
        {
            return new ArrayPtr<T>(x);
        }

        public static bool operator ==(ArrayPtr<T> x, ArrayPtr<T> y)
        {
            return x.source == y.source && x.index == y.index;
        }

        public static bool operator !=(ArrayPtr<T> x, ArrayPtr<T> y)
        {
            return !(x == y);
        }

        public override bool Equals(object x)
        {
            if (x == null) return this.source == null;
            var ptr = x as ArrayPtr<T>?;
            if (!ptr.HasValue) return false;
            return this == ptr.Value;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = this.source == null ? 0 : this.source.GetHashCode();
                return hash + this.index;
            }
        }

        public T this[int index]
        {
            get { return source[index + this.index]; }
            set { source[index + this.index] = value; }
        }
    }
}
