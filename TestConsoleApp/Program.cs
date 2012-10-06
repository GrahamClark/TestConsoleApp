using System;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //IRunner runner = new Currying.Runner();
                //runner.RunProgram();
                var o = M();
                if (o is System.Text.StringBuilder)
                {
                    var s = o as System.Text.StringBuilder;
                    Console.WriteLine(s.ToString());
                }
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

        static System.Runtime.Serialization.ISerializable M()
        {
            return new System.Text.StringBuilder();
        }
    }
}
