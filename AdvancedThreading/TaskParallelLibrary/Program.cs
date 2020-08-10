using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskParallelLibrary
{
    /// <summary>
    /// Task Parallel Library (TPL)
    /// - Is a set of public types and API that can be found on two namespaces:
    /// 1 System.Threading
    /// 2 System.Threading.Tasks 
    /// - Simplifies adding parallelism and concurrency to applications
    /// - Value is the ability to scale degree of concurrency dynamically (use all available processors efficiently)
    /// - Handles partitioning of work
    /// - Allows for task cancellation
    /// - Handles state management
    /// - Not all code is suitable for parallization
    /// - Threading of any type has an associated overhead
    /// - In some cases multithreading may be slower than sequential code. 
    /// </summary>
    
    


    class Program
    {
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($@"{i}");
            }
            Console.WriteLine($@"Time taken {stopWatch.ElapsedMilliseconds}");
            stopWatch.Stop();

            //in this instance the parallel executes slower
            stopWatch.Start();
            Parallel.For(0, 10, i => { Console.WriteLine($@"{i}"); });
            Console.WriteLine($@"Time taken {stopWatch.ElapsedMilliseconds}");

        }
    }
}
