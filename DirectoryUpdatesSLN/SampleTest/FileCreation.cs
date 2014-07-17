using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.qas.sambo.directoryupdate.SampleTest
{
    class FileCreation
    {
        public static void Main()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"text.txt"))
            {
                for (int i = 0; i < 100000000;i++ )
                    file.WriteLine("test");
            }
        }


        public static void ThreadProc(object Test)
        {
            int i = 0;
            while (true)
            {
                Console.Write("Getting there {0}", i++);
                Thread.Sleep(1000);
            }
        }
    }
}
