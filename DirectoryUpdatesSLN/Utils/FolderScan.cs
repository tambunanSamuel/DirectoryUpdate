using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace com.qas.sambo.directoryupdate.Utils
{
    class FolderScan
    {
        List<string> DirectoryList;
        List<string> ImagesList;

        [STAThread]
        public static void Main()
        {
            FolderScan fs = new FolderScan();
            Console.WriteLine("==========Folder Scan=========");
            Console.WriteLine(fs.CheckHasSetupFile(@"\\Product\product\World Data\NZL\v4"));
            Console.WriteLine(fs.CheckHasSetupFile(@"\\Product\product\World Data\NZL\v4\2014 07-July-M"));
            Console.WriteLine(fs.CheckHasSetupFile(@"\\Product\product\World Data\NZL\v4\2014 06-June-Q"));
            Console.WriteLine(fs.CheckHasSetupFile(@"\\Product\product\World Data\NZL\v4\2014 06-June-Q\CD Image"));
            Console.WriteLine(fs.CheckHasSetupFile(@"\\Product\product\World Data\NZL\v4\2014 06-June-Q\CD Image"));
            fs.WriteHasAddressDataSubdirectory(@"\\Product\product\World Data\NZL\v4\2014 06-June-Q");
            fs.WriteHasAddressDataSubdirectory(@"\\Product\product\World Data\Aus\v4\2014 04-April-Q\CD Images");
            fs.WriteHasAddressDataSubdirectory(@"\\Product\product\World Data\Usa\v4\2014 07-July-BM\CD Images");
        }

        public FolderScan()
        {
            DirectoryList = new List<string>();
            DirectoryList.Add("Address Data");
            DirectoryList.Add("Dataplus");
            DirectoryList.Add("Suppression Data");
            DirectoryList.Add("NON CASS");
            DirectoryList.Add("SERP");

            ImagesList = new List<string>();
            ImagesList.Add("CD Images");
        }

        public void WriteHasAddressDataSubdirectory(string path)
        {
            List<string> list = GetAddressDataSubdirectory(path);
            Console.WriteLine("Checking path {0}", path);
            foreach(string s in list)
            {
                Console.WriteLine("\t Has Subdirectory {0}",s);
            }
        }

        /// <summary>
        /// Checks if this directory path contains a Setup.exe file.
        /// The path given must not end in a "\"
        /// </summary>
        /// <param name="path">The directory path</param>
        /// <returns></returns>
        public bool CheckHasSetupFile(string path)
        {
            path = path + @"\Setup.exe";
            return File.Exists(path);
        }

        public bool CheckHasNZLSequenceFile(string path)
        {
            path = Path.Combine(path, "nzlseq.dap");
            return File.Exists(path);
        }

        /// <summary>
        /// Returns a list of string that matches the directories within 
        /// the DirectoryList. If no subdirectories match the list in DirectoryList
        /// an empty list will be returned
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetAddressDataSubdirectory(string path)
        {
            List<string> returnedValue = new List<string>();
            foreach(string dir in DirectoryList)
            {
                if (Directory.Exists(path+"\\"+dir))
                {
                    returnedValue.Add(dir);
                }
            }

            return returnedValue;
        }

        /// <summary>
        /// Will return true if path path contains a subfolder of folder
        /// </summary>
        /// <param name="path">Directory Path</param>
        /// <param name="folder">Folder</param>
        /// <returns></returns>
        public bool ContainsSubFolder(string path, string folder)
        {
            return Directory.Exists(Path.Combine(path,folder));
        }
    }
}
