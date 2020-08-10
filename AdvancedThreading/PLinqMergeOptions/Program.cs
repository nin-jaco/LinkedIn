using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLinqMergeOptions
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Enumerable.Range(1, 10);
            var query = list.AsParallel().WithMergeOptions(ParallelMergeOptions.AutoBuffered).Select(p => p);


        }
    }
}
