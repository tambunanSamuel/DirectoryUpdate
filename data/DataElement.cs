using System;


/// This class is used as a Data Object for the Data Directory application
namespace com.qas.sambo.directoryupdate.data
{
	public class DataElement {
		private string sourcePath;
		private DateTime lastModified;
		private string destinationPath;
		
		
		public DataElement() {
			sourcePath = "";
			destinationPath = "";
			lastModified = default(DateTime);
		}
		
		public DataElement(string sourcePath, string destinationpath) {
			SourcePath = sourcePath;
			DestinationPath = destinationPath;
			lastModified = default(DateTime);
		}
		
		public string SourcePath
		{
			get
			{
				return sourcePath;
			}
			set
			{
				sourcePath = value;
			}
		}
		
		public DateTime LastModified
		{
			get
			{
				return lastModified;
			}
			set
			{
				lastModified = value;
			}
		}
		
		public string DestinationPath
		{
			get
			{
				return destinationPath;
			}
			set
			{
				destinationPath = value;
			}
		}
	}
}
