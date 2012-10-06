using System;
using System.Collections.Generic;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;

namespace TestConsoleApp.AutoPoco
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
                {
                    x.Conventions(c => c.UseDefaultConventions());
                    x.AddFromAssemblyContainingType<Person>();
                    x.Include<Person>()
                     .Setup(c => c.Title).Use<RandomAlphaStringSource>(2, 4)
                     .Setup(c => c.Name).Use<FirstNameSource>()
                     .Setup(c => c.Age).Use<RandomIntegerSource>(21, 70);
                });

            IGenerationSession session = factory.CreateSession();

            IList<Person> people = session.List<Person>(10).Get();

            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
        }
    }
}
