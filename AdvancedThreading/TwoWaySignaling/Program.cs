using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwoWaySignaling
{
    class Program
    {
        static EventWaitHandle first = new AutoResetEvent(false);
        static EventWaitHandle second = new AutoResetEvent(false);
        static object helloWorldLock = new object();
        private static string _value = string.Empty;

        static void Main(string[] args)
        {
            Task.Factory.StartNew(WorkerThread);
            Console.WriteLine("Main thread is waiting.");
            first.WaitOne();

            lock (helloWorldLock)
            {
                _value = "Updating the value in the main thread.";
                Console.WriteLine(_value);
            }
            Thread.Sleep(2500);
            //return signal to release
            second.Set();
            Console.WriteLine("Released worker thread.");
        }

        private static void WorkerThread()
        {
            Thread.Sleep(2500);
            lock (helloWorldLock)
            {
                _value = "Updating the value in the worker thread.";
                Console.WriteLine(_value);
            }
            //first signal to release
            first.Set();
            Console.WriteLine("Released main thread.");
            Console.WriteLine("Worker thread is waiting");
            second.WaitOne();

        }
    }
}
