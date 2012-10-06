using System;
using System.Security.Cryptography;
using System.Text;

namespace TestConsoleApp.PasswordHashing
{
    internal sealed class HashProcessor : IDisposable
    {
        private HashAlgorithm _transform;
        private readonly string _systemSalt;
        private readonly ulong _iterations;

        public HashProcessor(ulong iterations, string systemSalt)
        {
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException("iterations", "iterations cannot be less than 1");
            }

            if (String.IsNullOrWhiteSpace(systemSalt))
            {
                throw new ArgumentNullException("systemSalt");
            }

            _iterations = iterations;
            _systemSalt = systemSalt;
            _transform = SHA512Managed.Create();
        }

        public void Dispose()
        {
            if (_transform != null)
            {
                _transform.Dispose();
            }
        }

        public string HashPassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            string salt = GenerateSalt();
            return HashPassword(password, salt);
        }

        public bool Verify(string password, string saltAndHash)
        {
            // get the salt off the hash
            string salt = saltAndHash.Substring(0, 32);
            string testHash = HashPassword(password, salt);

            return testHash == saltAndHash;
        }

        private string HashPassword(string password, string salt)
        {
            byte[] completeSalt = Encoding.UTF8.GetBytes(_systemSalt + salt);
            byte[] hash = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[0];

            for (ulong i = 0; i < _iterations; i++)
            {
                saltedPassword = CombineBytes(hash, completeSalt);
                hash = _transform.ComputeHash(saltedPassword);
            }

            return salt + BytesToHex(hash);
        }

        private static string GenerateSalt()
        {
            byte[] salt = new byte[16]; // 128 bits
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetNonZeroBytes(salt);
            }

            return BytesToHex(salt);
        }

        private static byte[] CombineBytes(byte[] first, byte[] second)
        {
            byte[] buffer = new byte[first.Length + second.Length];
            for (int i = 0; i < first.Length; i++)
            {
                buffer[i] = first[i];
            }

            for (int i = 0; i < second.Length; i++)
            {
                buffer[i + first.Length] = second[i];
            }

            return buffer;
        }

        /// <summary>
        /// Converts a byte array into a hexadecimal string.
        /// </summary>
        /// <param name="bytes">The bytes to convert.</param>
        /// <returns>A hexadecimal string.</returns>
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
