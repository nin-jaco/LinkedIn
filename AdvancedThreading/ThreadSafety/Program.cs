using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafety
{
    class Program
    {
        static Dictionary<int, string> items = new Dictionary<int, string>();
        
        static void Main(string[] args)
        {
            var task1 = Task.Factory.StartNew(AddItem);
            var task2 = Task.Factory.StartNew(AddItem);
            var task3 = Task.Factory.StartNew(AddItem);
            var task4 = Task.Factory.StartNew(AddItem);
            var task5 = Task.Factory.StartNew(AddItem);
            //order here does not matter
            Task.WaitAll(task1, task2, task3, task4, task5);
        }

        /// <summary>
        /// static methods can be thread safe in .net.  When creating static methods ensure that they are thread safe.
        /// </summary>
        private static void AddItem()
        {
            lock (items)
            {
                Console.WriteLine($@"Lock acquired by {Task.CurrentId}");
                items.Add(items.Count, $@"Hello World {items.Count}");
            }

            Dictionary<int, string> dictionary;
            lock (items)
            {
                Console.WriteLine($@"Lock 2 acquired by {Task.CurrentId}");
                dictionary = items;
            }

            foreach (var item in dictionary)
            {
                Console.WriteLine($@"Key: {item.Key}, Value:{item.Value}");
            }
        }
    }
}
