using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.EventHandling
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            Test1();
            Test2();
        }

        private void Test1()
        {
            Console.WriteLine("Test 1:");
            Server s = new Server();
            Client c = new Client();
            s.Initialize(c);
            c.RaiseEvent();
            c.RaiseEvent();

            s.Stop();
            c.RaiseEvent();
        }

        private void Test2()
        {
            Console.WriteLine("\nTest 2:");
            Server s = new Server();
            Client c = new Client();

            //add an extra delegate to the event. Now Stop() will be called before OnSomeEvent().
            //Stop() will remove the OnSomeEvent() handler from SomeEvent, but this won't take effect until next time
            //SomeEvent is raised. Adding and removing delegates inside an event call takes effect the *next* time.
            c.SomeEvent += 
                delegate(object sender, EventArgs e)
                {
                    s.Stop();
                };
            s.Initialize(c);
            c.RaiseEvent();
            s.Stop();
        }
    }
}
