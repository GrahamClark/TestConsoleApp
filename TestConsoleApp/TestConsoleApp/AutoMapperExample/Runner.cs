using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestConsoleApp.AutoMapperExample
{
    internal class Runner : IRunner
    {
        static Runner()
        {
            Mapper.CreateMap<A.Thing, B.MagicThing>()
                  .ForMember(b => b.DateLastModified, options => options.MapFrom(a => a.ChangedDate));
            Mapper.AssertConfigurationIsValid();
        }

        public void RunProgram()
        {
            DateTime date = DateTime.Today;

            var thing = new A.Thing()
            {
                Name = "thing",
                ChangedDate = date,
                Numbers = new List<int> { 2, 4, 6 }
            };

            var magicThing = Mapper.Map<A.Thing, B.MagicThing>(thing);

            Assert.IsNotNull(magicThing);
            Assert.AreEqual("thing", magicThing.Name);
            Assert.AreEqual(date, magicThing.DateLastModified);
            Assert.AreEqual(3, magicThing.Numbers.Count());
            Assert.AreEqual(2, magicThing.Numbers.ElementAt(0));
            Assert.AreEqual(4, magicThing.Numbers.ElementAt(1));
            Assert.AreEqual(6, magicThing.Numbers.ElementAt(2));

            Console.WriteLine("Translated object has the same values as the initial object.");
        }
    }
}
