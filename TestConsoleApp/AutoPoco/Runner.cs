using AutoPoco;
using AutoPoco.DataSources;
using System;

namespace TestConsoleApp.AutoPoco
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            var factory = AutoPocoContainer.Configure(
                x =>
                {
                    x.Conventions(c => c.UseDefaultConventions());
                    x.AddFromAssemblyContainingType<Person>();
                    x.Include<Person>()
                     .Setup(c => c.Title).Use<RandomAlphaStringSource>(2, 4)
                     .Setup(c => c.Name).Use<FirstNameSource>()
                     .Setup(c => c.Age).Use<RandomIntegerSource>(21, 70);
                });

            var session = factory.CreateSession();

            var people = session.List<Person>(10).Get();

            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
        }
    }
}
