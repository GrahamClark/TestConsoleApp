using System;

namespace TestConsoleApp.BinarySerialization.V1
{
    [Serializable]
    public class Person
    {
        private string name;
        private int age;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        public override string ToString()
        {
            return String.Format("Name: {0}\nAge: {1}", this.Name, this.Age);
        }
    }
}
