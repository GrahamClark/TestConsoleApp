using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.XsltExtensionObjects
{
    public class ComparisonRequest
    {
        public List<Product> ProductsRequested { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public List<Fund> FundsRequested { get; set; }
    }

    public class Fund
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
