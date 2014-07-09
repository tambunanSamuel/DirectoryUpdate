using System;
using com.qas.sambo.directoryupdate.data;

namespace com.qas.sambo.directoryupdate.data
{
	public class StaticDirectoryListings
	{
		public static DataElement NZLde;
		/* public StaticDirectoryListings() 
		{
			NZLde = new DataElement();
		} */
		
		public static void  init()
		{
			NZLde = new DataElement();
			NZLde.SourcePath = @"\\Product\product\World Data\NZL\v4";
			NZLde.DestinationPath = @"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder";
		}
		
	}
}