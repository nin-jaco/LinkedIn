using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEvent
{
	/// <summary>
    /// Signalling synchronizes shared resources among threads.
    /// Is used to notify another thread that it can not access a resource that was being used by the current thread.
    /// Three types of EventWaitHandle calls:
    /// 1. AutoResetEvent 
    ///     - Thread needs exclusive access to a resource.
    ///     - Only one thread can access a resource at a time.
    ///     - Automatically closes.
    ///     - A thread waits for a signal by calling WaitOne.
    ///     - Calling set signals release to waiting thread
    ///     - if multiple threads call WaitOne, a queue is formed.
    ///     - if Set is called when no thread us waiting, the handke stays open indefinitely.
    ///     - Calling Set only releases one thread at a time, regardless of how many times it is called.
    ///     - Can create an AutoResetEvent with initial state of "signaled" by passing "true" in the constructor parameter. "False" will be in non signalled state.
    /// 2. ManualResetEvent
    /// 3. CountDownEvent
    /// </summary>
	class Program
    {
        //static EventWaitHandle autoResetEvent = new EventWaitHandle(false,EventResetMode.AutoReset);
        static EventWaitHandle eventWaitHandle = new System.Threading.AutoResetEvent(false);

        static void Main(string[] args)
        {
            Task.Factory.StartNew(WorkerThread);
            Thread.Sleep(2500);
            eventWaitHandle.Set();
        }

        private static void WorkerThread()
        {
            Console.WriteLine($@"Waiting to enter the gate..");
            //the thread waits for the set
            eventWaitHandle.WaitOne();
            //when set happens
            Console.WriteLine("Gate entered..");
        }
    }
}
