using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace com.qas.sambo.directoryupdate.data
{
    class ThreadTest
    {

        public static void Main()
        {
            ThreadTest t = new ThreadTest();
            t.testA();
            t.testB();
        }
        public void testA()
        {
            Console.WriteLine("Test A");
        }

        public void testB()
        {
            Console.WriteLine("Test B");
        }
    }
}
