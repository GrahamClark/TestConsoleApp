using System;

namespace TestConsoleApp.EventHandling
{
    internal class Client
    {
        public event EventHandler SomeEvent;

        public void RaiseEvent()
        {
            if (SomeEvent != null)
            {
                SomeEvent(this, null);
            }
        }
    }
}
