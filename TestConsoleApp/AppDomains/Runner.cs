using System;
using System.Reflection;

namespace TestConsoleApp.AppDomains
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            A local = new A() { Name = "local" };
            local.PrintAppDomain();

            AppDomain domain = AppDomain.CreateDomain("NewDomain");
            A remote = (A)domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, "TestConsoleApp.AppDomains.A");
            remote.Name = "remote";
            remote.PrintAppDomain();

            AppDomain.Unload(domain);
            try
            {
                remote.PrintAppDomain();
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("domain is unloaded");
            }

            A newRemote = (A)domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, "TestConsoleApp.AppDomains.A");
            newRemote.Name = "new remote";
            newRemote.PrintAppDomain();

            remote.PrintA(local);
            remote.PrintAppDomain();
        }
    }

    // only one of [Serializable] and MarshalByRefObject is required, they should ideally be mutually exclusive to avoid confusion.
    [Serializable]
    public class A : MarshalByRefObject
    {
        public string Name { get; set; }

        public void PrintAppDomain()
        {
            Console.WriteLine("In AppDomain {1}", this.Name, AppDomain.CurrentDomain.FriendlyName);
        }

        public void PrintA(A a)
        {
            Console.WriteLine(a.ToString());
        }

        public override string ToString()
        {
            return String.Format("A : {0}", this.Name);
        }
    }
}
