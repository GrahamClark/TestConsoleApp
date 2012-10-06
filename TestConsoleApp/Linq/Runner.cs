using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.Linq
{
    class Runner : IRunner
    {
        private static List<Item> list1 = 
            new List<Item>()
            {
                new Item {Name="First", Size=3, Home="here"},
                new Item {Name="Second", Size=19, Home="there"},
                new Item {Name="Third", Size=23, Home="nowhere"}
            };
        private static List<Item> list2 = 
            new List<Item>()
            {
                new Item {Name="First", Size=3},
                new Item {Name="Fourth", Size=21},
                new Item {Name="Third", Size=23}
            };

        public void RunProgram()
        {
            //Test1();
            //Console.WriteLine();
            //Test2();
            Test3();
        }

        private static void Test1()
        {
            Console.WriteLine("*** Test 1 ***\n");
            List<int> list = new List<int> { 1, 3, 2, 4 };

            var query1 = from num in list
                         where num < 4
                         select num;

            var query2 = from num in query1
                         where num > 1
                         select num;

            var query3 = from num1 in query1
                         from num2 in query2
                         select num1 + num2;

            Console.WriteLine("Query 1:");
            foreach (var item in query1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nQuery 2:");
            foreach (var item in query2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nQuery 3:");
            foreach (var item in query3)
            {
                Console.WriteLine(item);
            }
        }

        private static void Test2()
        {
            Console.WriteLine("*** Test 2 ***\n");

            var query = list1.Except<Item>(list2, new Comparer());

            foreach (Item item in query)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private void Test3()
        {
            IEnumerable<char> query = "Not what you might expect";
            IEnumerable<char> query2 = "Not what you might expect";
            IEnumerable<char> vowels = "aeiou";

            query = query.Where(c => c != 'a')
                         .Where(c => c != 'e')
                         .Where(c => c != 'i')
                         .Where(c => c != 'o')
                         .Where(c => c != 'u');

            foreach (var item in query)
            {
                Console.Write(item);
            }

            Console.WriteLine();

            foreach (char vowel in "aeiou")
            {
                query2 = query2.Where(c => c != vowel);
            }

            foreach (var item in query2)
            {
                Console.Write(item);
            }

            Console.WriteLine();
        }
    }

    class Item
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Home { get; set; }

        public override string ToString()
        {
            return "Name = " + this.Name + "\nSize = " + this.Size + "\n";
        }
    }

    class Comparer : IEqualityComparer<Item>
    {
        public bool Equals(Item x, Item y)
        {
            return (x.Name == y.Name && x.Size == y.Size);
        }

        public int GetHashCode(Item obj)
        {
            Item i = obj as Item;
            return i.Name.Length ^ i.Size;
        }
    }
}
