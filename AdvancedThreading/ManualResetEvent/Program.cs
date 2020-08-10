using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEvent
{
    class Program
    {
        private static System.Threading.ManualResetEvent manualWaitHandle = new System.Threading.ManualResetEvent(false);
        //private static EventWaitHandle waitHandle = new System.Threading.ManualResetEvent(false);
        
        
        static void Main(string[] args)
        {
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);

            Thread.Sleep(2500);
            Console.WriteLine("Press a key to release all of the threads so far.");
            Console.ReadKey();
            //has to call set manually
            manualWaitHandle.Set();
            Thread.Sleep(2500);

            Console.WriteLine("Press a key again. The threads wont block event if they called WaitOne.");
            Console.ReadKey();
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Thread.Sleep(2500);

            Console.WriteLine("Press a key again. The threads will block again if they called WaitOne.");
            Console.ReadKey();
            manualWaitHandle.Reset();
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Task.Factory.StartNew(CallWaitOne);
            Thread.Sleep(2500);

            Console.WriteLine("Press a key again. The threads will be released.");
            Console.ReadKey(); 
            manualWaitHandle.Set();
            Console.ReadLine();
        }

        private static void CallWaitOne()
        {
            Console.WriteLine($@"{Task.CurrentId} has called waitOne.");
            manualWaitHandle.WaitOne();
            Console.WriteLine($@"{Task.CurrentId} has finally ended.");
        }
    }
}
