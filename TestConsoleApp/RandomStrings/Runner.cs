using System;
using System.Security.Cryptography;
using System.Text;

namespace TestConsoleApp.RandomStrings
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            var buffer = new byte[128];
            using (var cryptoRandom = new RNGCryptoServiceProvider())
            {
                cryptoRandom.GetNonZeroBytes(buffer);
            }

            Console.WriteLine(BytesToHex(buffer));
        }

        private static string BytesToHex(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                // append the byte in hex format
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
