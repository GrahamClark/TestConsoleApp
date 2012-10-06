using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.Lazy
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var orderFetcher = new OrderFetcher();

            Console.WriteLine("Fetching order info - non-lazy");
            var ordersNonLazy = orderFetcher.FetchOrders();
            var orderNonLazy = orderFetcher.FetchOrder();

            Console.WriteLine("\nFetching order info - lazy");
            var ordersLazy = new Lazy<IList<Order>>(() => { return orderFetcher.FetchOrders(); });
            var orderLazy = new Lazy<Order>(() => { return orderFetcher.FetchOrder(); });

            Console.WriteLine("Created lazy objects");
            Console.WriteLine("Has data been fetched for List? - {0}", ordersLazy.IsValueCreated);
            Console.WriteLine("Has data been fetched for Order? - {0}", orderLazy.IsValueCreated);

            Console.WriteLine("Using lazy objects");
            var orders = ordersLazy.Value;
            var order = orderLazy.Value;
            Console.WriteLine("Has data been fetched for List? - {0}", ordersLazy.IsValueCreated);
            Console.WriteLine("Has data been fetched for Order? - {0}", orderLazy.IsValueCreated);
        }
    }
}
