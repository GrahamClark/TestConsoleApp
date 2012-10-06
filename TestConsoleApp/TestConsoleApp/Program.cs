using System;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IRunner runner = new YieldReturns.Runner();
                runner.RunProgram();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("\ndone.\n");
                Console.ReadKey();
            }
        }
    }
}
