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
        private static DataElement USAde;

        private static string FilePath;

        private static Dictionary<string, DataElement> DeList;

        public static void init()
        {

           // CreateDataElements();

            //Serializing to a file
            FilePath = ReadSetting("JsonFileLocation");

            //Update
            //UpdateFile(DeList);
        }

        private static void CreateDataElements()
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

            AUGde = new DataElement();
            AUGde.SourcePath = @"\\Product\product\World Data\AUG";
            AUGde.DestinationPath = new List<string> {@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\AUG"};
            AUGde.LastModified = new DateTime(2014, 5, 05);
            AUGde.ElementName = "AUG";

            AUEde = new DataElement();
            AUEde.SourcePath = @"\\Product\product\World Data\AUE";
            AUEde.DestinationPath = new List<string> {@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\AUE"};
            AUEde.LastModified = new DateTime(2014, 5, 05);
            AUEde.ElementName = "AUE";

            NZGde = new DataElement();
            NZGde.SourcePath = @"\\Product\product\World Data\NZG\v4";
            NZGde.DestinationPath = new List<string> {@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\NZG"};
            NZGde.LastModified = new DateTime(2014, 5, 05);
            NZGde.ElementName = "NZG";

            SGFde = new DataElement();
            SGFde.SourcePath = @"\\Product\product\World Data\SGF\v4";
            SGFde.DestinationPath = new List<string> {@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\SGF"};
            SGFde.LastModified = new DateTime(2014, 5, 05);
            SGFde.ElementName = "SGF";

            //USA should only dl BMs
                     USAde = new DataElement();
            USAde.SourcePath = @"\\Product\product\World Data\Usa\v4";
            USAde.DestinationPath = new List<string> {@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\USA"};
            USAde.LastModified = new DateTime(2014, 5, 05);
            USAde.ElementName = "USA";


            DeList = new Dictionary<string, DataElement>();
            DeList.Add("NZL", NZLde);
            DeList.Add("AUS", AUSde);
            DeList.Add("AUG",AUGde);
            DeList.Add("AUE",AUEde);
            DeList.Add("NZG",NZGde);
            DeList.Add("SGF",SGFde);
            DeList.Add("USA", USAde);

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

        /// <summary>
        /// Will update the file with the Dictionary
        /// </summary>
        /// <param name="DeList">the dictionary list to update the file </param>
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

            Dictionary<string, DataElement> test = new Dictionary<string, DataElement>();
            UpdateFile(DeList);
            test = ReadJson();

            foreach (string s in test.Keys)
            {
                Console.WriteLine("Key is {0}", s);
            }
        }

        /// <summary>
        /// Returns a Dictionary that contains the information from the File in the FilePath
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, DataElement> ReadJson()
        {
            DataElement test = JsonConvert.DeserializeObject<DataElement>(System.IO.File.ReadAllText(FilePath));
            string fileRead = System.IO.File.ReadAllText(FilePath);
            Dictionary<string,DataElement> myDeList = JsonConvert.DeserializeObject<Dictionary<string,DataElement>>(fileRead);
            return myDeList;
        }

    }
}