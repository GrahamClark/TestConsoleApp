using System;
using System.Collections;

namespace TestConsoleApp.EventHandling
{
    internal class Server
    {
        private Client client;
        private EventHandler handler;
        private ArrayList list;

        public void Initialize(Client c)
        {
            client = c;
            list = new ArrayList();
            // listen to the event on bar
            handler = new EventHandler(OnSomeEvent);
            client.SomeEvent += handler;
        }

        private void OnSomeEvent(object sender, EventArgs args)
        {
            // event was raised, do something
            // this can't happen if Stop() was called.... right?
            list.Add("blah");
            Console.WriteLine("OnSomeEvent handled.");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        public void Stop()
        {
            // tidy up, stop listening to the event and clear the list
            client.SomeEvent -= handler;
            list = null;
        }
    }
}
