using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using com.qas.sambo.directoryupdate;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace com.qas.sambo.directoryupdate.data
{
	public class DirectoryUpdateFile 
	{
        private string logFileDirectory;

		public static void Main() 
		{
		
		
			string dirFrom = @"C:\MyFiles\Programming\Testing\CopyFolder1";
			string dirTo = @"C:\MyFiles\Programming\Testing\CopyFolder3";
			
			DirectoryUpdateFile df = new DirectoryUpdateFile();
		//	df.SourcePath=@"\\Product\product\World Data\NZL\v4";
		
		
			Console.WriteLine("Copying dir from {0} to {1}", dirFrom, dirTo);
			//df.CopyDirectories(dirFrom,dirTo);
            Console.WriteLine(dirTo);
            dirTo = " ";
            //dirTo = (dirTo.Length-1!='\\') ? 
            Console.WriteLine("is the last string \\? {0}", dirTo.ElementAt(dirTo.Length - 1) == '\\');
            dirTo = (dirTo.ElementAt(dirTo.Length - 1)) == '\\' ? dirTo : dirTo + "\\"; 
            Console.WriteLine("Last element of dirTo is {0}",dirTo.ElementAt(dirTo.Length-1));
            dirTo = dirTo + "\\";
            Console.WriteLine("is the last string \\? {0}", dirTo.ElementAt(dirTo.Length-1) == '\\');
			
			try {
				string dirpath = @"\\Product\product\World Data";
				dirpath = @"\\Product\product\World Data\NZL\v4";
				List<string> dirs = new List<string>(Directory.EnumerateDirectories(dirpath));
				Console.WriteLine("ff" == "fg");
                DateTime dtTest = new DateTime(2014,2,05);
         
				/*foreach (var dir in dirs)
				{
				
				Console.WriteLine("{0} was created at {1} and is newer? {2}", dir, Directory.GetCreationTime(dir),df.CheckIfNewer(dir,dtTest));
					
				/*DateTime dt = Directory.GetCreationTime(dir);
					//CheckMainDirectory(dir);
					Console.WriteLine("{0} was created at {1}", dir, Directory.GetCreationTime(dir));
					//Console.WriteLine("{0}", dir.Substring(dir.LastIndexOf("\\")+1));
					
					// Only print if it is newer than a certain date
					
				}
				Console.WriteLine("{0} directories found.", dirs.Count);
				*/
			}
			catch(Exception e)
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
			String folderName = dir.Substring(dir.LastIndexOf("\\")+1);
			return true;
		}
		
		private void TestCopyDirectory()
		{
			
		}
		
		
		private void CopyDirectories(string SourceDir, string NewPath)
		{
			// Using Visual Basic 
			try {
				new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(SourceDir,NewPath,true);
			}
			catch(System.InvalidOperationException e)
			{
				Console.WriteLine("Cannot write file. Please check source and destination path:\n\n {0}",e.ToString());
			}
            catch(System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine("The source/destination directory has not been found", e.ToString());
            }

		}
		
		
		///
		/// CheckIfNewer will check whether the date modified of Directory dir
		/// is newer than the dtValue provided
		///
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
        /// This will write to the log file with a file called Log.txt
        /// in the directory of LogFileDirectory
        /// </summary>
        private void WriteLogFile()
        {
            if (LogFileDirectory== null)
            {

                Console.WriteLine("Please set the Log File Directory");
                Console.WriteLine("Executing Assembly is {0}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Console.WriteLine("Get Entry Assembly is {0}", Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

            } else
            {

            }
        }


	}
}
