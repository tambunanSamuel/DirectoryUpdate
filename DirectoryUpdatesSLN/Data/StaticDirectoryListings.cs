using System;
using com.qas.sambo.directoryupdate.data;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Configuration;

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

        private static string FilePath;

        public static Dictionary<string, DataElement> DeList;

        /* public StaticDirectoryListings() 
        {
            NZLde = new DataElement();
        } */

        public static void init()
        {
            // NZL datasets
            NZLde = new DataElement();
            NZLde.SourcePath = @"\\Product\product\World Data\NZL\v4";
            NZLde.DestinationPath = new List<string> { @"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\NZL", @"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder2\NZL" };
            NZLde.LastModified = new DateTime(2014, 5, 05);
            NZLde.ElementName = "NZL";

            AUSde = new DataElement();
            AUSde.SourcePath = @"\\Product\product\World Data\Aus\v4";
            AUSde.DestinationPath = new List<string> { @"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\AUS" };
            AUSde.LastModified = new DateTime(2014, 5, 05);
            AUSde.ElementName = "AUS";

            DeList = new Dictionary<string, DataElement>();
            DeList.Add("NZL", NZLde);
            DeList.Add("AUS", AUSde);


            //Serializing to a file
            FilePath = ReadSetting("JsonFileLocation");
        }

        private static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Datasets.json";
                return result;
            }
            catch (ConfigurationErrorsException Cee)
            {
                Console.WriteLine("Error reading app settings. Hence using a default value of: Datasets.json");
                return "Datasets.json";
            }
        }
        /// <summary>
        /// Will be used to check if the JSON file exist or not
        /// </summary>
        public static bool CheckFileExist()
        {
            return System.IO.File.Exists(FilePath);
        }

        public static void CreateFile()
        {
            using (System.IO.File.Create(FilePath)) ;

        }

        public static void UpdateFile(Dictionary<string,DataElement> DeList)
        {
            System.IO.File.WriteAllText(FilePath, JsonConvert.SerializeObject(DeList, Formatting.Indented));
        }

        public static void Main()
        {
            init();
            Console.WriteLine("Testing if FIles exists {0}", CheckFileExist());
            Console.WriteLine("Creating file");
            CreateFile();
            Console.WriteLine("Testing if FIles exists {0}", CheckFileExist());

            UpdateFile(DeList);
            ReadJson();
        }

        /// <summary>
        /// Returns a Dictionary from the File in the FilePath
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, DataElement> ReadJson()
        {
            DataElement test = JsonConvert.DeserializeObject<DataElement>(System.IO.File.ReadAllText(FilePath));
            string fileRead = System.IO.File.ReadAllText(FilePath);
            Dictionary<string,DataElement> myDeList = JsonConvert.DeserializeObject<Dictionary<string,DataElement>>(fileRead);
            return myDeList;
        }

    }
}