using System;


/// This class is used as a Data Object for the Data Directory application
namespace com.qas.sambo.directoryupdate.data
{
	public class DataElement {
        private string elementName;
		private string sourcePath;
		private DateTime lastModified;
		private string[] destinationPath;
		
		
		public DataElement() {
            sourcePath = "";
            destinationPath = null;
            elementName = "";
			lastModified = default(DateTime);
		}

        public DataElement(string elementName, string sourcepath, params string[] destinationPath)
        {
            ElementName = elementName;
			SourcePath = sourcePath;
			DestinationPath = destinationPath;
			lastModified = default(DateTime);
		}

        public string ElementName
        {
            get 
            {
                return elementName;
            }
            set
            {
                elementName = value;
            }
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
		
		public string[] DestinationPath
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

        public override string ToString()
        {
            string s = "\nElement name: " + ElementName;
            s = s + "\n Source Path: " + SourcePath;
            if (DestinationPath != null)
            {
                foreach (string dest in DestinationPath)
                    s = s + "\n Destination Path: " + dest;
            }
            else
            {
                s = s + "\n Destination Path: NULL";
            }
            s = s +"\n Last Modified: " + LastModified;
            return s;
        }
        
	}
}
