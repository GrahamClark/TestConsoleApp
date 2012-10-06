using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using TestConsoleApp.Properties;

namespace TestConsoleApp.Xsl
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            IXPathNavigable input = this.GetInputXml();
            
            var nav = input.CreateNavigator();
            Console.WriteLine("input = " + nav.OuterXml);

            var stylesheet = this.GetStylesheet();
            XsltArgumentList args = new XsltArgumentList();
            
            using (MemoryStream ms = this.TransformXml(input, stylesheet))
            {
                StreamReader sr = new StreamReader(ms);
                Console.WriteLine("transformed = " + sr.ReadToEnd());
                ms.Position = 0;

                XmlReader xr = XmlReader.Create(ms);
                xr.MoveToContent();
            }
        }

        private IXPathNavigable GetInputXml()
        {
            var xml = new XDocument(new XElement("x", new XElement("y", "sds")));

            //XmlDocument xml = new XmlDocument();
            //xml.LoadXml("<x><y>sds</y></x>");

            return xml.CreateNavigator();
        }

        private string GetQuoteData()
        {
            return "<quote>something</quote>";
        }

        private string GetUserDataXml()
        {
            return "<user><name>Bob</name></user>";
        }

        private XslCompiledTransform GetStylesheet()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Resources.XslStylesheet);

            XslCompiledTransform xsl = new XslCompiledTransform();
            xsl.Load(xml.CreateNavigator());
            xsl.TemporaryFiles.Delete();

            return xsl;
        }

        private MemoryStream TransformXml(
            IXPathNavigable xml,
            XslCompiledTransform stylesheet)
        {
            MemoryStream transformed = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(transformed);
            stylesheet.Transform(xml, null, writer);

            transformed.Position = 0;
            return transformed;
        }
    }
}
