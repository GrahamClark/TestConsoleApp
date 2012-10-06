using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace TestConsoleApp.DataContractSerialization
{
    class Runner : IRunner
    {
        const string PersonFile = "SavedPerson.xml";

        public void RunProgram()
        {
            //SavePerson();
            ReadPerson();
        }

        private static void SavePerson()
        {
            Person person = new Person() { Name = "Joe", Age = 35 };
            DataContractSerializer serializer = new DataContractSerializer(typeof(Person));
            using (FileStream fs = new FileStream(GetFileName(), FileMode.Create))
            {
                serializer.WriteObject(fs, person);
            }
        }

        private static void ReadPerson()
        {
            Person person = null;
            DataContractSerializer serializer = new DataContractSerializer(typeof(Person));
            using (FileStream fs = new FileStream(GetFileName(), FileMode.Open))
            {
                person = (Person)serializer.ReadObject(fs);
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
    }
}
