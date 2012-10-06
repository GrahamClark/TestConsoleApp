using System;

namespace TestConsoleApp.IEquatable
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var c1 = new C { Name = "n", Number = 1 };
            var c2 = new C { Name = "m", Number = 2 };

            var s1 = new S(1, 2);
            var s2 = new S(3, 4);

            Console.WriteLine(c1 == c2); // reference equality testing

            Console.WriteLine(c1.Equals(c2)); // IEquatable method used

            Console.WriteLine(s1.Equals(s2)); // IEquatable method used

            Console.WriteLine(Object.Equals(c1, c2)); // overridden Equals method used

            Console.WriteLine(Object.Equals(s1, s2)); // overridden Equals method used
        }
    }

    class C : IEquatable<C>
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public override bool Equals(object obj)
        {
            Console.WriteLine("overridden C.Equals");

            var c = obj as C;
            if (c == null)
            {
                return false;
            }

            return c.Name == this.Name && c.Number == this.Number;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("overridden C.GetHashCode");

            int hash = 17;
            hash = hash * 23 + this.Name.GetHashCode();
            hash = hash * 23 + this.Number.GetHashCode();
            return hash;
        }

        public bool Equals(C other)
        {
            Console.WriteLine("IEquatable C.Equals");

            return other.Name == this.Name && other.Number == this.Number;
        }
    }

    struct S : IEquatable<S>
    {
        private int first;
        private int second;

        public S(int f, int s)
        {
            this.first = f;
            this.second = s;
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("overridden S.Equals");

            if (obj is S)
            {
                var s = (S)obj;
                return s.first == this.first && s.second == this.second;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            Console.WriteLine("overridden S.GetHashCode");

            int hash = 17;
            hash = hash * 23 + this.first.GetHashCode();
            hash = hash * 23 + this.second.GetHashCode();
            return hash;
        }

        public bool Equals(S other)
        {
            Console.WriteLine("IEquatable S.Equals");

            return other.first == this.first && other.second == this.second;
        }
    }
}
