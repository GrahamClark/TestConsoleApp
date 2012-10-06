using System;

namespace TestConsoleApp.PasswordHashing
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            using (var hasher = new HashProcessor(64000, "uWhhXEP2ke"))
            {
                var hash = hasher.HashPassword("password");
                Console.WriteLine(hash.Length);
                Console.WriteLine(hash);

                Console.WriteLine(hasher.Verify("password", hash));
                Console.WriteLine(hasher.Verify("pwd", hash));
            }
        }
    }
}
