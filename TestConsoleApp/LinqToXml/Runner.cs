using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TestConsoleApp.LinqToXml
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            Console.WriteLine("Reading:\n");
            ReadXml();

            Console.WriteLine("\nWriting:\n");
            WriteXml();
        }

        private static void ReadXml()
        {
            string xmlContent = "<something><another>55</another></something>";
            XDocument insertContent = XDocument.Parse(xmlContent);
            XElement xml = new XElement("top",
                                new XElement("first",
                                    new XAttribute("name", "fred"),
                                    new XAttribute("age", "30"),
                                    new XElement("inner", "20")
                                ),
                                new XElement("second",
                                    new XAttribute("name", "jibber")
                                ),
                                XElement.Parse(xmlContent)
                            );

            Console.WriteLine(xml);

            var q = from d in xml.Nodes()
                    where d.NodeType == XmlNodeType.Element && ((XElement)d).Attribute("name") != null
                    select ((XElement)d).Attribute("name");

            foreach (string s in q)
            {
                Console.WriteLine(s);
            }
        }

        private static void WriteXml()
        {
            List<Person> people = new List<Person>()
            {
                new Person() { Name = "Alex", Age = 40 },
                new Person() { Name = "Chris", Age = 30 },
                new Person() { Name = "Adam", Age = 20 }
            };

            XDocument xml = new XDocument();
            XElement elements = new XElement("people",
                                    from p in people
                                    orderby p.Name
                                    where p.Name.StartsWith("a", StringComparison.CurrentCultureIgnoreCase)
                                    select new XElement("person",
                                                new XElement("name", p.Name),
                                                new XElement("age", p.Age))
                                );
            xml.Add(elements);
            Console.WriteLine(xml.ToString());
        }
    }
}
