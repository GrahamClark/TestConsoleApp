namespace TestConsoleApp.HashSet
{
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            var toCompare = obj as Person;
            if (toCompare == null)
            {
                return false;
            }

            return toCompare.Name == Name && toCompare.Age == Age;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + Age.GetHashCode();
                if (Name != null)
                {
                    hash = (hash * 23) + Name.GetHashCode();
                }

                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Age: {1}", Name, Age);
        }
    }
}
