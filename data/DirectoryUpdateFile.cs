using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using com.qas.sambo.directoryupdate;

namespace com.qas.sambo.directoryupdate.data
{
	public class DirectoryUpdateFile 
	{
		DirectoryUpdateFile df = new DirectoryUpdateFile();
		public static void Main(String []args) 
		{
			df.SourcePath=@"\\Product\product\World Data\NZL\v4";
			Console.WriteLine("Hello World");
			try {
				//string dirpath = @"N:\";
				string dirpath = @"\\Product\product\World Data";
				dirpath = @"\\Product\product\World Data\NZL\v4";
				List<string> dirs = new List<string>(Directory.EnumerateDirectories(dirpath));
				Console.WriteLine("ff" == "fg");
				foreach (var dir in dirs)
				{
				DateTime dt = Directory.GetCreationTime(dir);
					//CheckMainDirectory(dir);
					Console.WriteLine("{0} was created at {1}", dir, Directory.GetCreationTime(dir));
					//Console.WriteLine("{0}", dir.Substring(dir.LastIndexOf("\\")+1));
					
					// Only print if it is newer than a certain date
					
				}
				Console.WriteLine("{0} directories found.", dirs.Count);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
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
	}
}
