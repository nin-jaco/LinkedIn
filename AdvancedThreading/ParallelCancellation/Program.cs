using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelCancellation
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var list = Enumerable.Range(0, 100000000).ToArray();
            var cancellationTokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cancellationTokenSource.Token;
            //default = -1
            parallelOptions.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            Console.WriteLine("Press 'x' to cancel.");

            Task.Factory.StartNew(() =>
            {
                if (Console.ReadKey().KeyChar == 'x')
                {
                    cancellationTokenSource.Cancel();
                }
            });

            long total = 0;
            try
            {
                Thread.Sleep(200);
                //interlocked is used to access shared resources
                Parallel.For<long>(0, list.Length, parallelOptions, () => 0,
                    (count, parallelLoopState, subtotal) =>
                    {
                        parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                        subtotal += list[count]; //0+1+2+..9=45
                        return subtotal;
                    },
                    (x) => { Interlocked.Add(ref total, x); });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($@"Cancelled by user input {e.Message}");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
            Console.WriteLine($@"The final sum is {total}");
        }
    }
}
