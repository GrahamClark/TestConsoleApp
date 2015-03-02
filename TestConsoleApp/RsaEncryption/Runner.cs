using System;
using System.Security.Cryptography;
using System.Text;

namespace TestConsoleApp.RsaEncryption
{
    class Runner : IRunner
    {
        private const string _privateKey =
@"<RSAKeyValue>
   <Modulus>1KSla3pt+6ihr4OWjTM4snOIs0t/f4RDRKrMbssxYTyC1IY6Likc/1FeYos6fD6QfjmI+6IVIJon62hGhh0D6ViB0vIoM55Ofm33vmj0I1X6x88fuWJshSSgWOLK7dtLqyy1ulrAUpQ8D4cZ3doyEoTxMZmhSs2L4sE8QjfeyGU=</Modulus>
   <Exponent>AQAB</Exponent>
   <P>7OCgH7PkSrwx7G+h7Qf7XPMiv3EeesOH/V83ihaAG4la1TKxq5RqUoT9gRkV2W0UWAWtdFaqShtFwW30cwL5oQ==</P>
   <Q>5c8wJmUPGmmGzHUMJwo281YVn0ss+gwaqcwzMkPyRTI38/DTWZiC8Jq8ZmM3JHkdWVhN4xoF4wlKLRSHnaGARQ==</Q>
   <DP>mWu3ajEqaJlRwHBhMVOdI4u6csJCWoLwPlQAdeiy2qLw+OsXlijPYFkQlB/6PdPffE9ZE+PZ8ZuOZ4Te8er4YQ==</DP>
   <DQ>lhZ0yf6imItnAE1JfI3NSatlP73nR/9zwoWcwi1iIxMjO+yC/DcA/YbxmKUftHBtXJaxd6rdQWQlz79iuu5pSQ==</DQ>
   <InverseQ>JmOj19wFyTufutz66lQ3jrJv6KduSbSc7BmpkYP3vl4Q0mPjCCwKz73RfthJX8l0r/KcuM/z65/BLsEqhHRscA==</InverseQ>
   <D>ALa8x2uVyu55/HjYnIi7e/3fS5rJRshO4YAhR43vEIB/f+8jatxAeKrxQittetVK7uGKkC2vHHjoAWZMNk8KoFBWsCObuX93n5JIChn+2CxIuE6MRuYxTKFXLyV54zA6I5rR9tPetsxa5/6tLLtQleSf7eKy/xHixuhmL0AadgE=</D>
</RSAKeyValue>";

        private const string _publicKey =
@"<RSAKeyValue>
   <Modulus>1KSla3pt+6ihr4OWjTM4snOIs0t/f4RDRKrMbssxYTyC1IY6Likc/1FeYos6fD6QfjmI+6IVIJon62hGhh0D6ViB0vIoM55Ofm33vmj0I1X6x88fuWJshSSgWOLK7dtLqyy1ulrAUpQ8D4cZ3doyEoTxMZmhSs2L4sE8QjfeyGU=</Modulus>
   <Exponent>AQAB</Exponent>
</RSAKeyValue>";

        public void RunProgram()
        {
            string secret = "d206a780-a567-41fb-bed1-1ab037b3c6fe";
            string encrypted = Encrypt(secret);
            Console.WriteLine(encrypted);

            string decrypted = Decrypt(encrypted);
            Console.WriteLine(decrypted);

            //Debug.Assert(decrypted == secret);
        }

        private string Encrypt(string secret)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_publicKey);
                var encoding = new UTF8Encoding();
                var encryptedBytes = rsa.Encrypt(encoding.GetBytes(secret), false);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        private string Decrypt(string encrypted)
        {
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                rsa.FromXmlString(_privateKey);
                var decryptedBytes = rsa.Decrypt(Convert.FromBase64String(encrypted), false);

                var encoding = new UTF8Encoding();
                return encoding.GetString(decryptedBytes);
            }
        }
    }
}
