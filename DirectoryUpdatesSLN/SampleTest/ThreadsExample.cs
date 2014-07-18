using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace com.qas.sambo.directoryupdate.SampleTest
{
    class ThreadsExample
    {
        static int num = 50;
        static bool myBoolean = true;
        public static void Main() {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), myBoolean);

            for (int i = 0; i < num; i++ )
                Console.WriteLine("Main thread does some work, then sleeps. with {0}",i);

            myBoolean = false;
            //Thread.Sleep(1000);
            Console.WriteLine("Main thread exits");

            
            Thread.Sleep(1000);
            for (int i = 0; i < 10; i++)
                Console.WriteLine("Thread POol should have stopped");
        }

        /// <summary>
        /// With threadpools, the thread created or child thread will stop running when the main thread is done. Threadpool uses background threads which do not allow 
        /// the running of new Files
        /// </summary>
        /// <param name="stateInfo"></param>
        static void ThreadProc(Object stateInfo)
        {
            
            int i = 0;
            while (ThreadsExample.myBoolean)
            {
                Console.WriteLine("Hello from the thread pool.{0}", i);
                i++;
                
            }
                
        }


    }
}
