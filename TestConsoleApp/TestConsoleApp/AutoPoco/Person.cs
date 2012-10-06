using System;

namespace TestConsoleApp.AutoPoco
{
    internal class Person
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}, age {2}", this.Title, this.Name, this.Age);
        }
    }
}
