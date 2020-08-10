using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCompletion
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            var taskCompletionsSource = new TaskCompletionSource<Product>();
            var lazyTask = taskCompletionsSource.Task;

            Task.Factory.StartNew(() =>
            {
                taskCompletionsSource.SetResult(new Product {Id = 1, ProductName = "Software Development"});
            });

            Task.Factory.StartNew(() =>
            {
                if (Console.ReadKey().KeyChar == 'x')
                {
                    var result = lazyTask.Result;
                    Console.WriteLine($"\n Result is {result.ProductName}.");
                }
            });

            Thread.Sleep(5000);
            Console.ReadLine();
        }
    }
}
