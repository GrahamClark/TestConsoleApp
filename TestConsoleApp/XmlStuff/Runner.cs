using System;
using System.Xml;

namespace TestConsoleApp.XmlStuff
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            string xml = "<top>hello<first>there</first><second>you</second></top>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            Console.WriteLine("XML " + xml);
            Console.WriteLine("from top node:");
            Console.WriteLine("Outer XML = " + doc.FirstChild.OuterXml);
            Console.WriteLine("Inner Text = " + doc.FirstChild.InnerText);
            Console.WriteLine("Inner XML = " + doc.FirstChild.InnerXml);
            Console.WriteLine("Value = " + doc.FirstChild.Value);
            Console.WriteLine("FirstChild Value = " + doc.FirstChild.FirstChild.Value);
        }
    }
}
