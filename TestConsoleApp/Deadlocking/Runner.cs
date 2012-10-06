using System.Threading;

namespace TestConsoleApp.Deadlocking
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            // 1. this will cause the static constructor for Deadlocking to be called
            Deadlocking.Start();
        }
    }

    class Deadlocking
    {
        static Deadlocking()
        {
            // run the initialization on another thread
            // 2. CLR takes out a lock that isn't released until this static ctor finishes
            // 3. When Initialize is called on another thread, the static ctor must be called. CLR tries to take the lock
            var thread = new Thread(Initialize);
            thread.Start();

            // 4. "thread" must now terminate, but can't because it's waiting on the main thread => deadlock.
            thread.Join();
        }

        static void Initialize()
        {
            // some initialization code...
        }

        public static void Start()
        {
        }
    }
}
