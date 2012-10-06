using System;
using System.Runtime.Serialization;

namespace TestConsoleApp.DataContractSerialization
{
    [DataContract]
    public class Person
    {
        private string personName;
        private int personAge;

        [DataMember(Order = 1)]
        public string Name
        {
            get { return this.personName; }
            set { this.personName = value; }
        }

        [DataMember(Order = 2)]
        public int Age
        {
            get { return this.personAge; }
            set { this.personAge = value; }
        }

        public override string ToString()
        {
            return String.Format("Name: {0}, Age: {1}", this.Name, this.Age);
        }
    }
}
