using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TapPattern
{
    class Program
    {
        /// <summary>
        /// Task-Based Asynchronous Pattern (TAP)
        /// - Returns Task or Task<TResult>
        /// - use Async suffix
        /// - accepts cancellation token	
        /// - returns quickly to the caller
        /// - frees up the thread if I/O bound
        /// </summary>
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(4000);
                tokenSource.Cancel();
            });

            DownloadAsync(new Uri("https://jsonplaceholder.typicode.com/posts"), token);
            Console.ReadLine();
        }

        public static async Task DownloadAsync(Uri uri, CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                try
                {
                    HttpResponseMessage result = await GetAsync(uri);
                    Console.WriteLine($@"Result is {result}.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static Task<HttpResponseMessage> GetAsync(Uri uri)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(uri);
            return result;
        }
    }
}
