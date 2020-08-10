using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLinqForAll
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Enumerable.Range(2000, 5000);
            var query = list.AsParallel().Where(x => x % 25 == 0);

            //var query2 = list.AsParallel().AsOrdered().Where(x => x % 25 == 0);
            var concurrentBag = new ConcurrentBag<int>();
            query.ForAll(x => concurrentBag.Add(x));

            //concurrency has the  advantage of providing the results as they are available but not ordered
            Console.WriteLine(concurrentBag.Count);
        }
    }
}
