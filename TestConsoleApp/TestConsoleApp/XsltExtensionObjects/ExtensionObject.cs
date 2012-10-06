using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace TestConsoleApp.XsltExtensionObjects
{
    public class ExtensionObject
    {
        public string GetProductId()
        {
            return "2";
        }

        public XPathNodeIterator GetNodeSet(string xPath)
        {
            ComparisonRequest request = new ComparisonRequest()
            {
                ProductsRequested = new List<Product>()
                {
                    new Product()
                    { 
                        Id = 1, 
                        FundsRequested = new List<Fund>()
                        {
                            new Fund() { Id = "dsfjds", Name = "sdjhuuwuw" },
                            new Fund() { Id = "iririr", Name = "vmnvmv" }
                        }
                    },
                    new Product()
                    {
                        Id = 2,
                        FundsRequested = new List<Fund>()
                        {
                            new Fund() { Id = "ffdjfjfjfj", Name = "ddioeoe" },
                            new Fund() { Id = "diiowiw", Name = "dsjwiw" }
                        }
                    },
                    new Product()
                    {
                        Id = 3,
                        FundsRequested = new List<Fund>()
                        {
                            new Fund() { Id = "odioiwi", Name = "iioie" },
                            new Fund() { Id = "ieieie", Name = "wuiwosk" }
                        }
                    }
                }
            };

            ObjectXPathNavigator nav = new ObjectXPathNavigator(request);

            XPathNodeIterator i = nav.Select(xPath);
            return i;
        }
    }
}
