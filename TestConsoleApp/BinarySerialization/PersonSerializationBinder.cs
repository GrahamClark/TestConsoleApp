using System;
using System.Runtime.Serialization;

namespace TestConsoleApp.BinarySerialization
{
    public class PersonSerializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return typeof(V2.Person);
        }
    }
}
