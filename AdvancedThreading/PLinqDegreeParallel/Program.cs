using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PLinqDegreeParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            var websites = new List<string>
            {
                "www.apple.com",
                "www.google.com",
                "www.microsoft.com"
            };

            var responses = websites.AsParallel().WithDegreeOfParallelism(websites.Count).Select(PingSites).ToList();
            foreach (var response in responses)
            {
                Console.WriteLine($@"Address: {response.Address}, Status: {response.Status}, Time Taken: {response.RoundtripTime}");
            }
            Console.ReadLine();
        }

        private static PingReply PingSites(string websiteName)
        {
            var ping = new Ping();
            return ping.Send(websiteName);
        }
    }
}
