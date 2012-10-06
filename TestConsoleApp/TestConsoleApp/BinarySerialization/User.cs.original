using System;

namespace TestConsoleApp.BinarySerialization
{
    [Serializable]
    public class User
    {
        private string name;
        private DateTime lastLoggedIn;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public DateTime LastLoggedIn
        {
            get { return this.lastLoggedIn; }
            set { this.lastLoggedIn = value; }
        }

        public override string ToString()
        {
            return String.Format("Name: {0}\nLast Logged In: {1}", this.name, this.lastLoggedIn);
        }
    }
}
