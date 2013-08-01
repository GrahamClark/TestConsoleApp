using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Text;

namespace TestConsoleApp.JsonSerialization
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var json = "{ \"people\": [ { \"name\": \"Bob\", \"age\": 40 }, { \"name\": \"Joe\", \"age\": 30 } ] }";
            var census = JsonSerializer.DeserializeFromString<Census>(json);
            foreach (var person in census.People)
            {
                Console.WriteLine(person);
            }
        }
    }
}
