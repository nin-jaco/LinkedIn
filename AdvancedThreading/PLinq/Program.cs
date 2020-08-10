using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLinq
{
    class Program
    {
        /// <summary>
        /// Parallel LINQ (PLINQ)
        /// - Automates parallelization
        /// - Is declaritive vs imperative
        /// - Operations that will prevent : take, Select, SelectMany, Skip, TakeWhile, SkipWhile, ElementAt
        /// - Parellized queries are not always faster than regular queries
        /// - Anomalies: Join, GroupBy, GroupJoin, Distinct, Union, Intersect, Except
        /// - Parallel queries can sometimes be executing sequentially
        /// - Force parallelism by calling the following after AsParallel(); .withExecutionMode (ParallelExecution.ForceParallelism)
        /// - PLINQ can be used to parallelize I/O-intensive operations like API and database calls
        /// - 
        /// </summary>
        static void Main(string[] args)
        {
            var list = Enumerable.Range(1, 100000);
            //may refactor and extract x here as a method
            var primalNumbers = list.AsParallel().Where((x) =>
            {
                if (x == 1) return false;
                if (x == 2) return true;
                if (x % 2 == 0) return false;
                var boundary = (int) Math.Floor(Math.Sqrt(x));

                for (int i = 3; i <= boundary; i += 2)
                {
                    if (x % i == 0)
                    {
                        return false;
                    }
                }

                return true;
            });
            Console.WriteLine($@"{primalNumbers.Count()} prime numbers found.");
        }
    }
}
