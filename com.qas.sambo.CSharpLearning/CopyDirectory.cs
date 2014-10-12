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
            IEnumerable<string> files = Directory.GetFiles(oldDirectory,"*",SearchOption.AllDirectories);
            IEnumerable<string> dirList = Directory.GetDirectories(oldDirectory, "*", SearchOption.AllDirectories);
            


            foreach (var f in dirList)
            {
                try
                {
                    Directory.CreateDirectory(f.Replace(oldDirectory, newDirectory));
                }
                catch(UnauthorizedAccessException uae)
                {
                    Console.WriteLine("Make sure you have permissions\n {0}", uae.Message);
                }
            }

            CopyFileExTest cfet = new CopyFileExTest();
            foreach (var f in files)
            {
                string newFilePath = f.Replace(oldDirectory, newDirectory);
                //Console.WriteLine("Files {0} to be replaced with {1}\n", f, newFilePath);
                cfet.XCopy(f, newFilePath);
            }
        }

        public static void Main()
        {
            //CopyDirectory.CopyDirectoryMethod(@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder", @"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder3");
            CopyDirectory.CopyDirectoryMethod(@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\AUE\2014 08-August-Q\CD Image\Data", @"\\aux1psv02-qas\DataBuilds\AUE\AUE August 2014 Data Subfolder");
        }
    }
}
