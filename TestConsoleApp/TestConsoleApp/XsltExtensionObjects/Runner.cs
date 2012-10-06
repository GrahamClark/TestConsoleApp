using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace TestConsoleApp.XsltExtensionObjects
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            XslCompiledTransform transformer = new XslCompiledTransform(true);
            using (StringReader reader = new StringReader(ExtensionObjectResources.transform))
            {
                transformer.Load(new XmlTextReader(reader));
            }

            XDocument doc = XDocument.Parse(ExtensionObjectResources.input);

            StringBuilder output = new StringBuilder();
            using (XmlWriter writer = XmlTextWriter.Create(output, new XmlWriterSettings() { Indent = true }))
            {
                XsltArgumentList arguments = new XsltArgumentList();
                arguments.AddExtensionObject("urn:ExtensionObject", new ExtensionObject());

                transformer.Transform(doc.CreateReader(), arguments, writer);
                transformer.TemporaryFiles.Delete();
            }

            Console.WriteLine(output.ToString());
        }
    }
}
