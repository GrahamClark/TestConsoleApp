using System;

namespace TestConsoleApp.NullableTypes
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            Values? e = null;
            e = Values.Third;

            if(e == Values.Third)
            {
                Console.WriteLine("it's First");
            }

            if(e.Value == Values.Third)
            {
                //Null Reference Exception
                Console.WriteLine("it's First");
            }

            if(!e.HasValue)
            {
                Console.WriteLine("it's null");
            }
        }
        enum Values
        {
            First,
            Second,
            Third
        }
    }

}
