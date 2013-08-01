using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TestConsoleApp.XmlSerialization
{
    class Runner : IRunner
    {
        const string PersonFile = "SavedPerson.xml";

        public void RunProgram()
        {
            //SerializationTest();
            //SavePerson();
            //ReadPerson();

            ServiceStackSerialization();
        }

        private void ServiceStackSerialization()
        {
            var person = new Person() { Name = "Bob", Age = 35, Children = new List<string> { "Chris", "Alice" } };

            var xml = new XDocument();
            var stream = new MemoryStream();
            using (var reader = new StreamReader(stream))
            {
                ServiceStack.Text.XmlSerializer.SerializeToStream(person, stream);

                xml = XDocument.Load(reader);
            }

            var deserialisedPerson = ServiceStack.Text.XmlSerializer.DeserializeFromString<Person>(xml.ToString());
            Console.WriteLine(person);
            Console.WriteLine(deserialisedPerson);
        }

        private static void SavePerson()
        {
            var person = new Person() { Name = "Bob", Age = 35, Children = new List<string> { "Chris", "Alice" } };
            var serializer = new XmlSerializer(typeof(Person));
            using (var fs = new FileStream(GetFileName(), FileMode.Create))
            {
                serializer.Serialize(fs, person);
            }
        }

        private static void ReadPerson()
        {
            Person person = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            using (FileStream fs = new FileStream(GetFileName(), FileMode.Open))
            {
                person = (Person)serializer.Deserialize(fs);
            }

            if (person != null)
            {
                Console.WriteLine(person.ToString());
            }
        }

        private static string GetFileName()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            return assemblyLocation.Substring(0, assemblyLocation.LastIndexOf('\\') + 1) + PersonFile;
        }

        private static void SerializationTest()
        {
            Person p1 = new Person() { Name = "Person 1", Age = 20 };
            Person p2 = new Person() { Name = "Person 2", Age = 60 };

            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            serializer.Serialize(Console.OpenStandardOutput(), p1);

            Console.WriteLine();
            Console.WriteLine();

            serializer.Serialize(Console.OpenStandardOutput(), p2);
        }
    }
}
