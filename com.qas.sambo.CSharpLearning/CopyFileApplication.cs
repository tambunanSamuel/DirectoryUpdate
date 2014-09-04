#region Using Namespaces 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace com.qas.sambo.CSharpLearning
{
    class CopyFileApplication
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CopyFileEx();


        public static void Main()
        {
            Console.WriteLine("Copy File Test");
            CopyFileApplication.CopyFileEx();
            CopyFileApplication.CopyFile(@"C:\MyFiles\Programming\Testing\CopyFolder1\text.txt","C:\MyFiles\Programming\Testing\CopyFolder1\text2.txt",true);

        }

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool CopyFile();
    }
}
