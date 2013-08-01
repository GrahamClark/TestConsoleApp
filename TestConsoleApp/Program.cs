using System;
using System.Web;

namespace TestConsoleApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                IRunner runner = new FileResources.Runner();
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
