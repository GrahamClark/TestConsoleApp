using System;

namespace TestConsoleApp.Workflow
{
    public class Person
    {
        public string Name { get; set; }

        public bool IsAnonymous
        {
            get { return String.IsNullOrEmpty(this.Name); }
        }
    }
}
