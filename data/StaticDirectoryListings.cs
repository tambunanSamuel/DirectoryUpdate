using System;
using com.qas.sambo.directoryupdate.data;
using System.Collections.Generic;

namespace com.qas.sambo.directoryupdate.data
{
	public static class StaticDirectoryListings
	{
		private static DataElement NZLde;
		private static DataElement AUSde;
		private static DataElement AUGde;
		private static DataElement NZGde;
		private static DataElement AUEde;
		private static DataElement SGFde;
		
		public static Dictionary<string,DataElement> DeList;
		
		/* public StaticDirectoryListings() 
		{
			NZLde = new DataElement();
		} */
		
		public static void  init()
		{
			NZLde = new DataElement();
			NZLde.SourcePath = @"\\Product\product\World Data\NZL\v4";
			NZLde.DestinationPath = @"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder";
			
			
			
			DeList = new Dictionary<string, DataElement>();
			DeList.Add("NZL",NZLde);
		}
		
	}
}