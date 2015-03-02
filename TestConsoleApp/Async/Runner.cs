using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestConsoleApp.Async
{
    class Runner : IRunner
    {
        public async void RunProgram()
        {
            Console.WriteLine("starting");
            var length = await GetResponseLength("http://www.google.com");
            Console.WriteLine("length = " + length);
            Console.WriteLine("after first async call");
        }

        private async Task<int> GetResponseLength(string url)
        {
            try
            {
                var client = new HttpClient();
                string urlContents = await client.GetStringAsync(url);
                Console.WriteLine("awaiting content retrieval");

                return urlContents.Length;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            finally
            {
                Console.WriteLine("in finally");
            }
        }
    }
}
