using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuationWithState
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<DateTime> task = Task.Run(() => DoSomething());
            var continuationTasks = new List<Task<DateTime>>();
            for (int i = 0; i < 3; i++)
            {
                task = task.ContinueWith((x, y) => DoSomething(), new Person {Id = i});
                continuationTasks.Add(task);
            }

            task.Wait();

            foreach (var continuationTask in continuationTasks)
            {
                Person person = continuationTask.AsyncState as Person;
                Console.WriteLine($@"Task finished at {continuationTask.Result}. Person id is {person.Id} ");

            }
        }

        public static DateTime DoSomething()
        {
            return DateTime.Now;
        }
    }
}
