namespace TestConsoleApp.CodeContracts
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var r = new Rational(2, 0);
            System.Console.WriteLine(r.Denominator);
        }
    }
}
