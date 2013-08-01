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
            var length = await GetRequestLength("http://www.google.com");
            Console.WriteLine("length = " + length);
            Console.WriteLine("after async call");
        }

        private async Task<int> GetRequestLength(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                Task<string> getStringTask = client.GetStringAsync(url);
                string urlContents = await getStringTask;

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
