using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;

namespace TestConsoleApp.XmlSerialization
{
    [Serializable]
    [XmlRoot("APerson")]
    public class Person
    {
        private int age;

        [XmlElement("PersonName")]
        public string Name { get; set; }

        [XmlElement("PersonAge")]
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                AgeSpecified = age < 50;
            }
        }

        [XmlArrayItem("Child")]
        public List<string> Children { get; set; }

        /// <summary>
        /// If <c>True</c>, the Age property will be serialised. If <c>False</c>, it won't be.
        /// </summary>
        [XmlIgnore]
        public bool AgeSpecified { get; set; }

        public override string ToString()
        {
            StringBuilder children = new StringBuilder();
            foreach (var child in this.Children)
            {
                children.Append(child + ", ");
            }

            return String.Format("Name: {0}, Age: {1}, Children: {2}", this.Name, this.Age, children.ToString());
        }
    }
}
