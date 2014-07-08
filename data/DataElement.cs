using System;

namespace com.qas.sambo.directoryupdate.data
{
	public class DataElement {
		private string SourcePath;
		private DateTime LastModified;
		private string DestinationPath;
		
		
		public DataElement() {
			SourcePath="";
			//LastModified=null;
			DestinationPath = "";
		}
	}
}
