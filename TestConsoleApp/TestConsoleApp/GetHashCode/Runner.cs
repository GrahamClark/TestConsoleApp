namespace TestConsoleApp.GetHashCode
{
    class Runner : IRunner
    {
        public void RunProgram()
        {

        }
    }

    /// <summary>
    /// GetHashCode algorithm taken from Jon Skeet: http://stackoverflow.com/a/263416/77090
    /// </summary>
    class A
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public override int GetHashCode()
        {
            // Overflow is fine, just wrap
            unchecked
            {
                int hash = 17; // start with a prime
                hash = hash * 23 + this.Name.GetHashCode();
                hash = hash * 23 + this.Number.GetHashCode();
                // repeat for all fields (including checks for null when appropriate

                return hash;
            }
        }
    }
}
