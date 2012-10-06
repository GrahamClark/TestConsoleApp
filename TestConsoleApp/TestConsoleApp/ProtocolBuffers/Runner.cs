using System.IO;
using ProtoBuf;

namespace TestConsoleApp.ProtocolBuffers
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            Save();
            Load();
        }

        private static void Save()
        {
            var person = new Person()
            {
                Name = "Bob",
                Age = 40,
                Height = 200,
                Gender = Gender.Male,
                Address = { HouseNumber = 10, StreetName = "King Road" }
            };

            using (var file = File.Open(@"C:\tmp\person.bin", FileMode.OpenOrCreate))
            {
                Serializer.Serialize(file, person);
            }
        }

        private static void Load()
        {
            Person person = null;
            using (var file = File.OpenRead(@"C:\tmp\person.bin"))
            {
                person = Serializer.Deserialize<Person>(file);
            }

            if (person != null)
            {
                System.Console.WriteLine(person.ToString());
            }
        }
    }
}
