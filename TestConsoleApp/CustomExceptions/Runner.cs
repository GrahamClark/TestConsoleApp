using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp.CustomExceptions
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            CatchException(() => { throw new TestException("general"); });
            CatchException(() => { throw new SpecificTestException("specific", "details"); });
        }

        private void CatchException(Action action)
        {
            try
            {
                action();
            }
            catch (TestException testException)
            {
                Console.WriteLine(testException.ToString());
            }
        }
    }

    internal class TestException : Exception
    {
        public TestException(string message)
            : base(message)
        {
        }

        public override string ToString()
        {
            return string.Format("Message: {0}", Message);
        }
    }

    class SpecificTestException : TestException
    {
        public string Details { get; private set; }

        public SpecificTestException(string message, string details)
            : base(message)
        {
            Details = details;
        }

        public override string ToString()
        {
            return string.Format("{0}, Details: {1}", base.ToString(), Details);
        }
    }
}
