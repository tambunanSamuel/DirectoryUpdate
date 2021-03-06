﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.directoryupdate.Utils
{
    class FileWriter
    {
        private string _userName;
        private string _password;
        private string fileName;
        
        public void AppendToFile(string details)
        {
            System.IO.File.AppendAllText(fileName, details);
        }

        public string CreateDetails(string zipName, string password)
        {
            string ret;
            ret =  "\n==============";
            ret += "\nUserName: " + UserName;
            ret += "\nPassword: " + Password;
       
            ret += "\nZip File is: " + zipName;
            ret += "\n To extract it please use the following password: " + password;
            ret += "\n==============\n";

            return ret;
        }
        public FileWriter()
        {
           _userName = "PIF0011";
           _password = "Password";

           try
           {
               var appSettings = ConfigurationManager.AppSettings;
               fileName = appSettings["LogFileLocation"] ?? "NotFound.Log";
           }
           catch (ConfigurationErrorsException ee)
           {
               Console.WriteLine("Error reading app settings");
           }
        }


        public string UserName { get {return _userName;}
             }

        public string Password
        {
            get
            {
                return _password;
            }
        }
    }
}
