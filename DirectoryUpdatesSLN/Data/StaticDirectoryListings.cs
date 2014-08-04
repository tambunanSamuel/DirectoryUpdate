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
		
		public static void init()
		{
            // NZL datasets
			NZLde = new DataElement();
			NZLde.SourcePath = @"\\Product\product\World Data\NZL\v4";
            NZLde.DestinationPath = new String[] { @"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\NZL", @"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder2\NZL" };
            NZLde.LastModified = new DateTime(2014, 5, 05);
            NZLde.ElementName = "NZL";

            AUSde = new DataElement();
            AUSde.SourcePath = @"\\Product\product\World Data\Aus\v4";
            AUSde.DestinationPath = new String[] {@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\AUS"};
            AUSde.LastModified = new DateTime(2014, 5, 05);
            AUSde.ElementName = "AUS";

            DeList = new Dictionary<string, DataElement>();
			DeList.Add("NZL",NZLde);
            DeList.Add("AUS", AUSde);

		}
		
	}
}