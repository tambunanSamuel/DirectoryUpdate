using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.directoryupdate.Utils
{
    class JSONElementList
    {
        
        public string FTPUser { get; set; }
        public string FTPPass { get; set; }
        public string FTPLink { get; set; }
        public string FilePass { get; set; }
        public string FileName { get; set; }

        public JSONElementList()
        {
            FTPUser = "PIF001";
            FTPPass = "Pass";
            FTPLink = "ftpLink";
            FileName = "filename";
            FilePass = "Password";
        }
        public JSONElementList(string FTPUser, string FTPPass, string FTPLink, string FileName, string Password)
        {
            this.FTPUser = FTPUser;
            this.FTPPass = FTPPass;
            this.FTPLink = FTPLink;
            this.FileName = FileName;
            this.FilePass = FilePass;
        }

        public static void init()
        {
            JSONElementList AUS = new JSONElementList();
            JSONUtil<JSONElementList> jsonUtil = new JSONUtil<JSONElementList>(JSONElementList.ReadSetting("ElementList"));
            Dictionary<string, JSONElementList> testDictionary = new Dictionary<string, JSONElementList>();
            testDictionary.Add("AUS",AUS);
            jsonUtil.WriteToJsonFile(testDictionary);
        }




        private static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key];
                return result;
            }
            catch (ConfigurationErrorsException Cee)
            {
                Console.WriteLine("Error reading app settings. Hence using a default value of: Datasets.json");
                return key;
            }
        }

    }
}
