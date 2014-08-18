using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.directoryupdate.Utils
{
    /// <summary>
    /// JsonUtil of Type T
    /// </summary>
    /// <typeparam name="T">Type that will be written in the JSON File</typeparam>
    class JSONUtil<T>
    {
        private string JsonFile;

        public JSONUtil(string JsonFile)
        {
            this.JsonFile = JsonFile;
        }

        public string ReturnFileName()
        {
            return JsonFile;
        }

        public void WriteToJsonFile(IDictionary<string, T> dicList)
        {
            System.IO.File.WriteAllText(JsonFile, JsonConvert.SerializeObject(dicList, Formatting.Indented));
        }

        /// <summary>
        /// Returns an IDictionary that contains the information from the File in the FilePath
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, T> ReadJson()
        {
            string fileRead = System.IO.File.ReadAllText(JsonFile);
            Dictionary<string, T> myDeList = JsonConvert.DeserializeObject<Dictionary<string, T>>(fileRead);
            return myDeList;
        }

    }
}
