using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestConsoleApp.Async
{
    public class MultipleTasksRunner : IRunner
    {
        public void RunProgram()
        {
            GetCodes(new[] { "http://www.google.com", "http://gsdsaasd.com", "http://www.google.com/sdkhsd.txt" });
        }

        public void GetCodes(IEnumerable<string> addresses)
        {
            var tasks = addresses.Select(GetStatusCode).ToArray();
            var codes = Task.WhenAll(tasks).Result;

            foreach (var httpStatusCode in codes)
            {
                Console.WriteLine(httpStatusCode);
            }

        }

        private async Task<HttpStatusCode> GetStatusCode(string address)
        {
            var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(address);
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
