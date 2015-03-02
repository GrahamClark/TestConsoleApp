using System;
using System.Collections.Generic;

namespace TestConsoleApp.HashSet
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            var people = new List<Person>
                         {
                             new Person { Age = 30, Name = "Bob" },
                             new Person { Age = 20, Name = "Bob" },
                             new Person { Age = 30, Name = "Joe" },
                             new Person { Age = 20, Name = "Bob" }
                         };

            var hashSet = new HashSet<Person>();
            foreach (var person in people)
            {
                hashSet.Add(person);
            }

            foreach (var person in hashSet)
            {
                Console.WriteLine(person);
            }
        }
    }
}
