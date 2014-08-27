using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace com.qas.sambo.directoryupdate.Utils
{
    class FileStringFormatting
    {
        private string[] monthList = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };


        public static void Main()
        {
            Console.WriteLine("STring Formatting");
            string[] lines = System.IO.File.ReadAllLines("FileList.log");
            FileStringFormatting fsf = new FileStringFormatting();

            foreach (string s in lines)
            {
                Console.WriteLine(s);
                Console.WriteLine("File name is {0}", fsf.RemoveFileType(s));
                string fileName = fsf.RemoveFileType(s);
                
                List<string> splits = fsf.ReturnStringSplit(fileName);
                Console.WriteLine("Datset is {0}", splits.ElementAt(0));
                Console.WriteLine("Year is {0}", splits.ElementAt(1));
                string unformattedDAte = splits.ElementAt(2);
                Console.WriteLine("Month is {0}", fsf.GetMonth(unformattedDAte));
                Console.WriteLine("Does it contain extra elements {0}", fsf.ContainsExtraElements(splits));
                Console.WriteLine("The extra element is {0}", fsf.ReturnExtraElements(splits));
                Console.WriteLine();
            }
        }

        public FileStringFormatting()
        { }

        public string RemoveFileType(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }

        public string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public List<string> ReturnStringSplit(string fileName)
        {
            return fileName.Split().ToList<string>();
        }

        public bool ContainsExtraElements(List<string> listString)
        {
            if (listString.Count>3)
                return true;
            else
            return false;
        }

        public string ReturnExtraElements(List<string> listString)
        {
            string ret = "";
            
            for (int i = 3; i < listString.Count; i++)
                ret += listString.ElementAt(i) + " " ;
            // trims because the previous one adds a space to the end
            return ret.Trim();
        }


        //IfUnknownMonth add the month to the monthList
        public string GetMonth(string unformattedDate)
        {
            foreach (string month in monthList)
            {
                if (unformattedDate.ToLower().Contains(month.ToLower()))
                    return month;
            }
            return "UnknownMonth_" + unformattedDate;
        }
    }


}
