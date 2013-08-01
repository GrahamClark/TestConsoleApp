using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.HashCodes
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var set = new HashSet<C>();

            set.Add(new C { Number = 1, Name = "a" });
            set.Add(new C { Number = 1, Name = "b" });
            set.Add(new C { Number = 2, Name = "b" });
            set.Add(new C { Number = 1, Name = "b" }); // won't be added

            var list = set.ToList();
            foreach (var c in list)
            {
                Console.WriteLine(c);
            }
        }
    }

    class C
    {
        public int Number { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var toCompare = obj as C;
            if (toCompare == null)
            {
                return false;
            }

            return this.Name == toCompare.Name && this.Number == toCompare.Number;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + this.Number.GetHashCode();
                if (this.Name != null)
                {
                    hash = hash * 23 + this.Name.GetHashCode();
                }

                return hash;
            }
        }

        public override string ToString()
        {
            return String.Format("Number: {0}, Name: {1}", Number, Name);
        }
    }
}
