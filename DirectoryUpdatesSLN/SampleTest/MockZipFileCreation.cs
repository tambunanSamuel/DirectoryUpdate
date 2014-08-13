using com.qas.sambo.directoryupdate.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.directoryupdate.SampleTest
{
    class MockZipFileCreation
    {
        public static void Main()
        {
            Console.WriteLine("Mock zip file");
            FolderScan fs = new FolderScan();

            // Returns true
            Console.WriteLine(fs.ContainsSubFolder(@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\AUS\2014 04-April-Q\\", "Cd IMsages"));

            // Returns false
            Console.WriteLine(fs.ContainsSubFolder(@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\AUS\2014 04-April-Q\CD Images\Address Data", "Setup.exe"));
            ZipFile(@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\AUS\2014 04-April-Q");
            //ZipFile(@"\\Product\product\World Data\Usa\v4\2014 07-July-BM");
            ZipFile(@"C:\MyFiles\Downloads\NZL March 2014");
        }

        public static void ZipFile(string path)
        {
            string oldPath = path;
            FolderScan fs = new FolderScan();
            Console.WriteLine("Zip file Method");
            if (fs.ContainsSubFolder(path, "Cd Images"))
            {
                path = path + "\\Cd Images";
                List<string> list = fs.HasAddressDataSubdirectory(path);

                if (list.Count == 0)
                {
                    Console.Write("Has Zero");
                }
                else
                {
                    foreach(string s in list)
                    {
                        OriginalZipFile(path + "\\" + s);
                    }
                }
                
                if (fs.CheckHasSetupFile(path))
                {
                    Console.WriteLine("Has Setup file");
                    OriginalZipFile(oldPath);

                }
            }
        }

        public static void OriginalZipFile(string destPath)
        {
            ZipEncryption ze = new ZipEncryption();
            Console.WriteLine("Will zip up {0}", destPath);

            String randomGen = ze.EncryptionPasswordGenerator(8);
            //String newZip = Path.GetDirectoryName(destPath) + "\\" + Path.GetFileName(destPath) + ".zip";
            String newZip = Path.GetDirectoryName(destPath) + "\\" + Path.GetFileName(destPath) + ".zip";
            ze.ZipWithEncryption(destPath, randomGen, newZip);

            writeToLog(newZip, randomGen);
            //Console.WriteLine("Path is {0}", Path.GetDirectoryName(destPath)+"\\"+Path.GetFileName(destPath)+".zip");
            ///throw new NotImplementedException();
        }

        private static void writeToLog(string newZip, string randomGen)
        {
            FileWriter fw = new FileWriter();
            fw.AppendToFile("Created at" + DateTime.Now.ToShortDateString());
            fw.AppendToFile(fw.CreateDetails(newZip, randomGen));
        }
    }
}
