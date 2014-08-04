using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using com.qas.sambo.directoryupdate;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace com.qas.sambo.directoryupdate.Data
{
    public class DirectoryUpdateFile
    {
        private string logFileDirectory;

        public DirectoryUpdateFile()
        {

        }
        public static void Main()
        {


            string dirFrom = @"C:\MyFiles\Programming\Testing\CopyFolder1";
            string dirTo = @"C:\MyFiles\Programming\Testing\CopyFolder3";

            DirectoryUpdateFile df = new DirectoryUpdateFile();
            //	df.SourcePath=@"\\Product\product\World Data\NZL\v4";


            //Console.WriteLine("Copying dir from {0} to {1}", dirFrom, dirTo);
            ////df.CopyDirectories(dirFrom,dirTo);
            //Console.WriteLine(dirTo);
            //dirTo = " ";
            ////dirTo = (dirTo.Length-1!='\\') ? 
            //Console.WriteLine("is the last string \\? {0}", dirTo.ElementAt(dirTo.Length - 1) == '\\');
            //dirTo = (dirTo.ElementAt(dirTo.Length - 1)) == '\\' ? dirTo : dirTo + "\\"; 
            //Console.WriteLine("Last element of dirTo is {0}",dirTo.ElementAt(dirTo.Length-1));
            //dirTo = dirTo + "\\";
            //Console.WriteLine("is the last string \\? {0}", dirTo.ElementAt(dirTo.Length-1) == '\\');

            try
            {
                string dirpath = @"\\Product\product\World Data";
                dirpath = @"\\Product\product\World Data\NZL\v4";
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(dirpath));
                DateTime dtTest = new DateTime(2014, 2, 05);

                foreach (var dir in dirs)
                {

                    Console.WriteLine("{0} was created at {1} and is newer? {2}", dir, Directory.GetCreationTime(dir), df.CheckIfNewer(dir, dtTest));

                    DateTime dt = Directory.GetCreationTime(dir);
                    //CheckMainDirectory(dir);
                    Console.WriteLine("{0} was created at {1}", dir, Directory.GetCreationTime(dir));
                    Console.WriteLine("{0}", dir.Substring(dir.LastIndexOf("\\") + 1));

                    // Only print if it is newer than a certain date

                }
                Console.WriteLine("{0} directories found.", dirs.Count);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            df.WriteLogFile();

        }

        /// <summary>
        /// Directory where the LogFile shoudl be located
        /// It will create a history.log
        /// </summary>
        public String LogFileDirectory
        {
            get
            {
                return logFileDirectory;
            }
            set
            {
                if (value != "")
                {
                    value = (value.ElementAt(value.Length - 1)) == '\\' ? value : value + "\\";
                }
                else
                {
                    logFileDirectory = value;
                }
            }
        }

        private static bool CheckMainDirectory(String dir)
        {
            String folderName = dir.Substring(dir.LastIndexOf("\\") + 1);
            return true;
        }

        private void TestCopyDirectory()
        {

        }

        /// <summary>
        /// Copy directoreis from SourceDir to NewPath using 
        /// Microsoft.visualbasic.devices.computer....CopyDirectory()
        /// </summary>
        /// <param name="SourceDir"></param>
        /// <param name="NewPath"></param>
        public void CopyDirectories(string SourceDir, string NewPath)
        {
            // Using Visual Basic 
            try
            {
                Console.WriteLine("Copying: {0}", SourceDir);

                new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(SourceDir, NewPath, true);
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine("Cannot write file. Please check source and destination path:\n\n {0}", e.ToString());
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine("The source/destination directory has not been found", e.ToString());
            }

        }

        /// <summary>
        /// Given a directory dir, this method will return the size of the directory.
        /// This function will call itself recursively to return the size of the folder
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public long GetDirectorySize(string dir)
        {
            long b = 0;
            List<string> AllDir = null;
            List<string> AllFiles = null;
            AllDir = new List<string>(Directory.EnumerateDirectories(dir));
            AllFiles = new List<string>(Directory.EnumerateFiles(dir));
            foreach (var folders in AllDir)
            {
                b += GetDirectorySize(folders);
            }
            foreach (var file in AllFiles)
            {
                FileInfo info = new FileInfo(file);
                //Console.WriteLine("Checking file {0} with bit size {1}", file, info.Length
                b += info.Length;
            }

            return b;

        }


        /// <summary>
        /// CheckIfNewer will check if the directory dir is newer than dtValue
        /// </summary>
        /// <param name="dir">the URL directory</param>
        /// <param name="dtValue">Date to be checked</param>
        /// <returns></returns>
        private bool CheckIfNewer(String dir, DateTime dtValue)
        {
            DateTime dtCurrent = Directory.GetCreationTime(dir);

            // it is greater than because the value of directory is more or greater than
            // what is checked upon. A newer directory will have a greater DateTime value
            // compared to an older directory
            if (dtCurrent > dtValue)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Will return a list of all directories in dir that is newer than dt
        /// </summary>
        /// <param name="dir">directory path</param>
        /// <param name="dt">date</param>
        /// <returns></returns>
        public List<String> ReturnDirectories(string dir, DateTime dt)
        {
            List<string> AllDir = new List<string>(Directory.EnumerateDirectories(dir));

            List<string> NewerDir = new List<string>();

            foreach (var dirToCheck in AllDir)
            {
                if (CheckIfNewer(dirToCheck, dt))
                {
                    NewerDir.Add(dirToCheck);
                }
            }
            return NewerDir;
        }


        /// <summary>
        /// This will write to the log file with a file called Log.txt
        /// in the directory of LogFileDirectory
        /// </summary>
        private void WriteLogFile()
        {
            if (LogFileDirectory == null)
            {

                Console.WriteLine("Please set the Log File Directory");
                Console.WriteLine("Executing Assembly is {0}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Console.WriteLine("Get Entry Assembly is {0}", Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                logFileDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            else
            {

            }
        }


    }
}
