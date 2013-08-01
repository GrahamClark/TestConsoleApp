using System;
using Moq;

namespace TestConsoleApp.MoqTests
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var mock1 = new Mock<C>(MockBehavior.Strict);
            var mock2 = new Mock<C>(MockBehavior.Strict);

            mock1.Setup(c => c.M()).Returns("bye");

            Console.WriteLine(mock1.Object.M());
            Console.WriteLine(mock2.Object.M());
        }
    }

    public class C
    {
        public virtual string M()
        {
            return "hello";
        }
    }
}
