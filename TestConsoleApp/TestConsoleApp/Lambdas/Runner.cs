using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.Lambdas
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            List<Person> people = new List<Person>()
            {
                new Person { FirstName = "Bob", LastName = "Smith" },
                new Person { FirstName = "Henry", LastName = "Smith" },
                new Person { FirstName = "Bob", LastName = "Jones" }
            };

            var forAllQuery = people.FindAll(delegate(Person p)
                                             {
                                                 return p.FirstName == "Bob";
                                             });

            var lambdaQuery = people.Where(p => p.FirstName == "Bob");

            var linqQuery = from p in people
                            where p.FirstName == "Bob"
                            select p;

            foreach (var item in forAllQuery)
            {
                Console.WriteLine(String.Format("{0} {1}", item.FirstName, item.LastName));
            }

            Console.WriteLine();

            foreach (var item in lambdaQuery)
            {
                Console.WriteLine(String.Format("{0} {1}", item.FirstName, item.LastName));
            }

            Console.WriteLine();

            foreach (var item in linqQuery)
            {
                Console.WriteLine(String.Format("{0} {1}", item.FirstName, item.LastName));
            }
        }

        private class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
