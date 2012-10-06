using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace TestConsoleApp.BinarySerialization.V2
{
    [Serializable]
    public class Person : ISerializable
    {
        private string forename;
        private int age;
        private int? height;

        public Person()
        {
        }

        private Person(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("name");
            this.Age = info.GetInt32("age");

            // no way to check if a specific value is in the SerializationInfo without throwing an exception,
            // so just go by the count.
            if (info.MemberCount == 3)
            {
                this.Height = (int?)info.GetValue("height", typeof(Nullable<int>));
            }
            else
            {
                // if the object being serialized doesn't have a height set, set a default.
                this.Height = 150;
            }
        }

        public string Name
        {
            get { return this.forename; }
            set { this.forename = value; }
        }

        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        public int? Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public override string ToString()
        {
            return String.Format("Name: {0}\nAge: {1}\nHeight: {2}", this.Name, this.Age, this.Height);
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // manually add everything to be serialized.
            info.AddValue("name", this.forename);
            info.AddValue("age", this.age);
            info.AddValue("height", this.height);
        }
    }
}
