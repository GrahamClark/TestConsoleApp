using System;
using System.Security.Cryptography;
using System.Text;

namespace TestConsoleApp.HMACSHA256Test
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            WritePassword(GetHash("password"));
            WritePassword(GetHash("djdsjssjdsfjkds"));
            WritePassword(GetHash("vljxliodjflisjeflsijeflsifj?D?!^$£%$"));
            WritePassword(GetHash("password"));
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

        private static void WritePassword(string password)
        {
            Console.WriteLine("{0} ({1})", password, password.Length);
        }

        private string GetHash(string password)
        {
            const string key = "key";
            var encoding = new UTF8Encoding();

            using (var hasher = new HMACSHA256(encoding.GetBytes(key)))
            {
                var hashedBytes = hasher.ComputeHash(encoding.GetBytes(password));
                return BytesToHex(hashedBytes);
            }
        }
    }
}
