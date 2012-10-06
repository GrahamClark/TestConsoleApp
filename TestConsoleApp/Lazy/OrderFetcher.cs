using System;
using System.Collections.Generic;

namespace TestConsoleApp.Lazy
{
    class OrderFetcher
    {
        public IList<Order> FetchOrders()
        {
            Console.WriteLine("About to fetch orders");

            return new List<Order>
            {
                new Order {OrderId = 1, InvoiceNumber = 1, OrderAmount = 19.95m, ItemCount = 2},  
                new Order {OrderId = 2, InvoiceNumber = 2, OrderAmount = 29.95m, ItemCount = 3},  
                new Order {OrderId = 3, InvoiceNumber = 3, OrderAmount = 33.25m, ItemCount = 4},  
                new Order {OrderId = 4, InvoiceNumber = 4, OrderAmount = 19.95m, ItemCount = 1},  
                new Order {OrderId = 5, InvoiceNumber = 5, OrderAmount = 204.17m, ItemCount = 10},  
                new Order {OrderId = 6, InvoiceNumber = 6, OrderAmount = 32.54m, ItemCount = 4}
            };
        }

        public Order FetchOrder()
        {
            Console.WriteLine("About to fetch single order");
            return new Order { OrderId = 1, InvoiceNumber = 1, OrderAmount = 19.95m, ItemCount = 2 };
        }
    }
}
