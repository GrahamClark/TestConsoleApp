using System;
using ProtoBuf;

namespace TestConsoleApp.ProtocolBuffers
{
    [ProtoContract]
    public class Person : Being
    {
        public Person()
        {
            this.Address = new Address();
            this.Type = "human";
        }

        [ProtoMember(1)]
        public override string Name { get; set; }

        [ProtoMember(2)]
        public int Age { get; set; }

        [ProtoMember(3)]
        public int Height { get; set; }

        [ProtoMember(4)]
        public Address Address { get; set; }

        [ProtoMember(5)]
        public Gender Gender { get; set; }

        public override string ToString()
        {
            return String.Format(
                "Name:\t{0}\nAge:\t{1}\nHeight:\t{2}\nGender:\t{3}\nAddress Street:\t{4}\n",
                this.Name, this.Age, this.Height, this.Gender.ToString(), this.Address.StreetName);
        }
    }
}
