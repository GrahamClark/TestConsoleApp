using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestConsoleApp.BinarySerialization
{
    internal class Runner : IRunner
    {
        private const string UserFile = "SavedUser.data";

        /// <summary>
        /// Serialize some data to a binary byte array.
        /// </summary>
        /// <typeparam name="TDataType">The type of data to serialize.</typeparam>
        /// <param name="data">The data to serialize.</param>
        /// <returns>The input data binary serialized.</returns>
        public static byte[] BinarySerialize<TDataType>(TDataType data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                IFormatter serializer = new BinaryFormatter();
                serializer.Serialize(memoryStream, data);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Deserializes binary data into a type.
        /// </summary>
        /// <typeparam name="TDataType">The type of data to deserialize.</typeparam>
        /// <param name="data">The data to deserialize.</param>
        /// <returns>The data represented by the binary input data.</returns>
        public static TDataType BinaryDeserialize<TDataType>(byte[] data, SerializationBinder binder)
        {
            TDataType newObj = default(TDataType);

            using (MemoryStream memStream = new MemoryStream(data))
            {
                IFormatter deserializer = new BinaryFormatter();
                if (binder != null)
                {
                    deserializer.Binder = binder;
                }

                newObj = (TDataType)deserializer.Deserialize(memStream);
            }

            return newObj;
        }

        public void RunProgram()
        {
            // PersonTest();
            Console.WriteLine();

            // WriteUser();
            ReadUser();
        }

        private static void PersonTest()
        {
            V1.Person person1 = new V1.Person() { Name = "Bob", Age = 52 };
            V2.Person person2 = new V2.Person() { Name = "Joe", Age = 25, Height = 200 };

            byte[] serialisedPerson1 = BinarySerialize<V1.Person>(person1);
            byte[] serialisedPerson2 = BinarySerialize<V2.Person>(person2);

            V2.Person deserialisedPerson1 = BinaryDeserialize<V2.Person>(serialisedPerson1, new PersonSerializationBinder());
            V2.Person deserialisedPerson2 = BinaryDeserialize<V2.Person>(serialisedPerson2, new PersonSerializationBinder());

            Console.WriteLine(deserialisedPerson1.ToString());
            Console.WriteLine();
            Console.WriteLine(deserialisedPerson2.ToString());
        }

        private static void WriteUser()
        {
            string filename = GetFileName();
            User user = new User() { Name = "Bob", LastLoggedIn = DateTime.Now };
            File.WriteAllBytes(filename, BinarySerialize<User>(user));
        }

        private static void ReadUser()
        {
            byte[] data = File.ReadAllBytes(GetFileName());
            User user = BinaryDeserialize<User>(data, null);
            Console.WriteLine(user.ToString());
        }

        private static string GetFileName()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            return assemblyLocation.Substring(0, assemblyLocation.LastIndexOf('\\') + 1) + UserFile;
        }
    }
}
