using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.CSharpLearning
{
    class CopyDirectory
    {
        public static void CopyDirectoryMethod(string oldDirectory, string newDirectory)
        {
            var dirList = from dir in Directory.EnumerateDirectories(oldDirectory)
                          let dirInfo = new DirectoryInfo(dir)
                          orderby dirInfo.CreationTime ascending
                          select dir;

            foreach (var f in dirList)
            {
                Console.WriteLine("Directory {0}",f);
            }
        }

        public static void Main()
        {
            CopyDirectory.CopyDirectoryMethod(@"C:\MyFiles","");
        }
    }
}
