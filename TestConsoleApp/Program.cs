using System;

namespace TestConsoleApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                IRunner runner = new CustomExceptions.Runner();
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
