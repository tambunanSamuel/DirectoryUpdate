using com.qas.sambo.directoryupdate.data;
using com.qas.sambo.directoryupdate.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using com.qas.sambo.directoryupdate.Utils;
using System.Configuration;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;


namespace com.qas.sambo.directoryupdate
{
    class DirectoryUpdate
    {
        DirectoryUpdateFile duf;
        Dictionary<string, DataElement> dictionaryList;
        Dictionary<string, JSONElementList> jsonElementList;
        JSONUtil<JSONElementList> jsonElementListFile;
        private String zipFileLocation, zipFileLocationKey = "ZipFileLocation";
        private string elementListAppConfig = "ElementList";
        private string datasetFile, datasetFileKey = "JsonFileLocation";

        public static void Main()
        {
            StaticDirectoryListings.init();

            DirectoryUpdate du = new DirectoryUpdate();
            du.init();
            du.checks();
            du.RunProgram();


        }

        private void checks()
        {
            // Put a check to ensure that zipFileLocation is a valid location

            // CreateFilesList();
        }

        private void CreateFilesList()
        {
            JSONUtil<DataElement> deJsonUtil = new JSONUtil<DataElement>(datasetFile);
            Dictionary<string, DataElement> dictionaryList = new Dictionary<string, DataElement>(deJsonUtil.ReadJson());

            JSONUtil<JSONElementList> jsonElementListJsonUtil = new JSONUtil<JSONElementList>(elementListAppConfig);
            Dictionary<string, JSONElementList> ftpList = new Dictionary<string, JSONElementList>(jsonElementListJsonUtil.ReadJson());

            foreach (string key in dictionaryList.Keys)
            {
                if (!ftpList.ContainsKey(key))
                {
                    ftpList.Add(key, new JSONElementList());
                }
            }
            jsonElementListJsonUtil.WriteToJsonFile(ftpList);
        }

        private void init()
        {
            string elementListFileLocation;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                zipFileLocation = appSettings[zipFileLocationKey] ?? @"C:\";
                datasetFile = appSettings[datasetFileKey];
                elementListFileLocation = appSettings[elementListAppConfig];
                jsonElementListFile = new JSONUtil<JSONElementList>(elementListFileLocation);
            }
            catch (ConfigurationErrorsException cee)
            {
                Console.WriteLine("Error reading app setting. Please ensure that ZipFileLocation is set");
            }


        }



        /// <summary>
        /// To run the Program
        /// </summary>
        private void RunProgram()
        {
            Console.WriteLine("==== Welcome to the Automatic Download Datasets Application ====");
            Console.WriteLine(@"Commands are:
D for download
S to show datasets
Q to quit");
            Console.WriteLine("Insert Command:");

            var command = Console.ReadLine().ToLower();

            String[] Inputs = SplitInput(command);

            while (!command.Equals("q"))
            {

                switch (Inputs[0])
                {
                    case "d":
                        Console.WriteLine("Will download");
                        DownloadDatasetsList();
                        Console.WriteLine("Finished Downloading");
                        //StaticDirectoryListings.UpdateFile(dictionaryList);
                        break;
                    case "s":
                        ShowDatasetsList();
                        break;
                    case "t":
                        if (Inputs[1].Equals(""))
                        {
                            Console.WriteLine("Second parameter is empty");
                            Console.WriteLine("Please input a second parameter");
                        }
                        else
                        {
                            Console.WriteLine("Finding size of Directory: ", Inputs[1]);
                            Console.WriteLine("Size is {0}", DirectorySize(Inputs[1]));
                        }

                        break;
                    case "e":
                        Console.WriteLine("Testing FileWriter");
                        FileWriter fs = new FileWriter();
                        fs.AppendToFile(fs.CreateDetails("zip1.zip", "fjoiej"));
                        break;

                    case "g":
                        //Console.WriteLine("Testing Zipping of Path");
                        //Console.WriteLine(Path.Combine(@"C:\MyFiles", "CD IMages"));
                        //Console.WriteLine(Path.Combine(@"C:\MyFiles", "\\CD IMages"));
                        //Console.WriteLine(Path.Combine(@"C:\MyFiles\", "CD IMages"));
                        //Console.WriteLine(Path.Combine(@"C:\MyFiles\\", "\\CD IMages"));
                        //Console.WriteLine(Path.Combine(@"C:\MyFile\", "CD IMages"));
                        //CheckSubDirectories(@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\AUS\2014 04-April-Q","AUS");
                        //CheckSubDirectories(@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder2\NZL\2014 06-June-Q","NZL");

                        //DirectoryUpdateFile duf = new DirectoryUpdateFile();
                        //List<string> myList = duf.ReturnDirectories(@"\\Product\product\World Data\Usa\v4", new DateTime(2014, 5, 5));

                        //var dirs = from file in myList
                        //           select new DirectoryInfo(file);


                        //var dirOrdered = from file in dirs
                        //                 orderby file.CreationTime ascending
                        //                 select file;

                        //var dirOrdered = from file in myList
                        //                 let fileInfo = new DirectoryInfo(file)
                        //                 orderby fileInfo.CreationTime ascending
                        //                 select file;

                        //List<DirectoryInfo> newList = new List<DirectoryInfo>();
                        //foreach (string file in myList)
                        //{
                        //    DirectoryInfo di = new DirectoryInfo(file);

                        //}

                        //foreach (var s in myList)
                        //    Console.WriteLine("Directory is {0} and Date created is {1}", s,Directory.GetCreationTime(s));

                        //foreach (var s in dirOrdered)
                        //    Console.WriteLine("Directory is {0} and Date is ",s);
                        //break;

                        CheckDirectory(@"\\Product\product\World Data\SGf\v4", new DateTime(2014, 04, 22));
                        break;
                    case "h":

                        JSONUtil<JSONElementList> js = new JSONUtil<JSONElementList>("FilesList.json");
                        //error because IDictionary is null

                        if (js.ReadJson() != null)
                        {
                            jsonElementList = new Dictionary<string, JSONElementList>(js.ReadJson());
                            Console.WriteLine("Is dictionary Empty? {0}", jsonElementList == null);

                        }

                        jsonElementList = new Dictionary<string, JSONElementList>(js.ReadJson());
                        Console.WriteLine("Is dictionary Empty? Now {0}", jsonElementList == null);
                        break;

                    case "r":
                        SFTPAccess sf = new SFTPAccess();
                        sf.AddFile("text.txt", "AUS");
                        long test = 5;
                        long gg = 4;
                        Console.WriteLine("{0:##}", (float)(gg / (test)));
                        break;

                    case "j":
                        Console.WriteLine("Testing WinForms");
                        //MessageBox.Show("Test");
                        Console.WriteLine("Before Output");
                        for (int i = 0; i < 10; i++)
                        {
                            int currentLine = Console.CursorTop;
                            Console.WriteLine("i is {0:0.00}", (float)i/10);
                            Thread.Sleep(500);
                            Console.SetCursorPosition(0, currentLine);
                        }
                        Console.WriteLine();
                        int statusLine = -1;
                        for (int i = 0; i < 10; i++)
                        {
                            if (i % 2 == 0)
                            {
                                Console.WriteLine("Other lines in here " + i);
                            }


                            Thread.Sleep(500);
                            //update the status line here
                            SetTextForLine("Status " + i + " Copied", ref statusLine);
                        }
                        Console.WriteLine();
                        Console.WriteLine("AFter Output");
                        break;
                    case "o":
                        Console.WriteLine("Testing Can CheckDirectory");
                        //CheckSubDirectories(@"C:\MyFiles\Programming\Testing\DirectoryUpdateTestFolder\CAN\2014 08-August-M", "CAN");
                        UploadSFTP us = new UploadSFTP();
                        List<string> myList = new List<string>();
                        using (StreamReader sr = new StreamReader("FileList.log"))
                        {
                            while (sr.Peek() > -1)
                            {
                                string line = sr.ReadLine();
                                Console.WriteLine("Line is {0} \n Dataset is {1}", line, us.GetDatasetName(line));
                                myList.Add(line);
         
                            }
                        }
                        Console.WriteLine("hoihoi");
                        var s = myList.AsEnumerable()
                            .Select(p=>p)
                            .Distinct();

                        SFTPAccess sa = new SFTPAccess();
                        foreach (var ff in s)
                        {
                            Console.WriteLine("Uploading {0} into subFolder {1}", ff, us.GetDatasetName(ff));
                        
                              //  sa.AddFile(ff, us.GetDatasetName(ff));
                    
                        }

                        sa.AddFile(@"C:\MyFiles\Programming\Testing\Zipped\AUE February 2014.zip", "AUE");
                        sa.AddFile("LogFile.log","AUE");
        
                        break;
                    case "p":
                        Console.WriteLine("Testing Copy Directory");
                        //try
                        //{
                        //    new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(@"C:\MyFiles\Programming\Testing\Zipped\AUS PAF 2014.4", @"C:\MyFiles\Programming\Testing\Zipped\AUS PAF 2014.45", UIOption.AllDialogs);
                        //}
                        //catch (System.InvalidOperationException e)
                        //{
                        //    Console.WriteLine("Cannot write file. Please check source and destination path:\n\n {0}", e.ToString());
                        //}
                        //catch (System.IO.DirectoryNotFoundException e)
                        //{
                        //    Console.WriteLine("The source/destination directory has not been found", e.ToString());
                        //}
                        FileInfo fi = new FileInfo(@"C:\MyFiles\Programming\Testing\Zipped\AUS July 2014 Dataplus.zip");
                        Console.WriteLine("Does exist {0}", fi.Exists);
                        if (fi.Exists)
                        {
                            Console.WriteLine(Path.Combine(Path.GetDirectoryName(fi.FullName),Path.GetFileNameWithoutExtension(fi.FullName)+"_"+"1"+".zip"));
                        }
                        fi = new FileInfo(@"C:\MyFiles\Programming\Testing\Zipped\AUS July 2014 Dataplus.zip31");
                        Console.WriteLine("Does exist {0}", fi.Exists);
                        break;
                    case "y":
                        Console.WriteLine("Testing ReturnDirectories2");
                        DirectoryUpdateFile duf = new DirectoryUpdateFile();
                        string st = "\\\\Aux1fsv02\\ndrive\\QAS\\product\\WorldData\\AUS";

                        var x = duf.ReturnTest(st, new DateTime(2014, 8, 28));
                        foreach (var f in x)
                            Console.WriteLine("path is {0} and time is {1}", f.ParentDirPath,f.NewestDT);

                        var latestDate = from f in x
                                         group f by f.ParentDirPath into g
                                         select g.OrderByDescending(t=>t.NewestDT).FirstOrDefault();
                        Console.WriteLine("ordered");
                        foreach (var f in latestDate)
                            Console.WriteLine("path is {0} and time is {1}", f.ParentDirPath, f.NewestDT);

                        Console.WriteLine("Using checkrecursive");
                        break;
                    default:

                        break;
                }

                Console.WriteLine("Insert Command:");
                command = Console.ReadLine().ToLower();
                Inputs = SplitInput(command);
            }

        }
        private void SetTextForLine(string text, ref int line)
        {
            //set the status line for future reference
            if (line < 0)
            {
                line = Console.CursorTop;
            }

            //save line/cursor state
            int currentLine = Console.CursorTop;
            bool cursorVisible = Console.CursorVisible;
            Console.SetCursorPosition(0, line);
            Console.WriteLine(text);

            //restore state
            Console.CursorTop = (currentLine == line ? currentLine + 1 : currentLine);
            Console.CursorVisible = cursorVisible;
        }

        private void CheckDirectory(string path, DateTime date)
        {

            var dirList = from dir in Directory.EnumerateDirectories(path)
                          let dirInfo = new DirectoryInfo(dir)
                          orderby dirInfo.CreationTime ascending
                          where dirInfo.CreationTime > date
                          select dir;

            foreach (string s in dirList)
            {
                Console.WriteLine("Dir {0} was created at {1}", s, Directory.GetCreationTime(s));
            }
        }

        /// <summary>
        /// Returns the directory size of directory dir. If -1 is returned then something would have gone wrong
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private long DirectorySize(string dir)
        {
            long DirSize = -1;
            try
            {
                DirectoryUpdateFile duf = new DirectoryUpdateFile();
                DirSize = duf.GetDirectorySize(dir);
                //Console.WriteLine("Bit size is {0}", String.Format("{0:n0}", duf.GetDirectorySize(dir)));
            }
            catch (System.IO.DirectoryNotFoundException sdnf)
            {
                Console.WriteLine("Directory not found {0}", sdnf.Message);
            }
            catch (System.UnauthorizedAccessException ue)
            {
                Console.WriteLine("Problem accessing directory {0}", ue.Message);
            }
            return DirSize;
        }

        /// <summary>
        /// Will download the datasets, 
        /// 1) will download to all the destination path
        /// 2) encrypt and zip
        /// 3) create log file
        /// 4) upload files
        /// </summary>
        private void DownloadDatasetsList()
        {
            dictionaryList = StaticDirectoryListings.ReadJson();
            foreach (var dicList in dictionaryList)
            {
                //Console.WriteLine("Key is {0}", dicList.Key);
                DataElement currDe = dicList.Value;
                string dataset = currDe.ElementName;

                //Task<List<string>> task1 = new Task<List<string>>(() => duf.ReturnDirectories(currDe.SourcePath, currDe.LastModified));
                Task<List<ParentDirectoryInfo>> task1 = new Task<List<ParentDirectoryInfo>>(() => duf.ReturnTest(currDe.SourcePath, currDe.LastModified));
                task1.Start();
                Console.WriteLine("Reading directory: {0}", currDe.SourcePath);
                while (!task1.IsCompleted)
                {
                    Console.Write(".");
                    Thread.Sleep(2000);
                }
                task1.Wait();
                
                Console.WriteLine();
                List<ParentDirectoryInfo> directoriesList = task1.Result;
                foreach(var f in directoriesList)
                {
                    Console.WriteLine("List of DIrectories: {0}", f.ParentDirPath);
                }
                if (directoriesList.Count == 0)
                {
                    Console.WriteLine("Nothing to Update for the {0} dataset\n", currDe.ElementName);
                }
                foreach (var fg in directoriesList)
                {
                    bool first = true;
                    var s = fg.ParentDirPath;
                    foreach (var destPath in currDe.DestinationPath)
                    {
                        var newDestPath = destPath + "\\" + Path.GetFileName(s);
                        //Console.WriteLine("NewDestPath is {0}", newDestPath);

                        Console.WriteLine("Copying {0} into {1}", s, newDestPath);

                        //Getting size of directory
                        Task<long> OriginalDestSize = new Task<long>(() => DirectorySize(s));
                        OriginalDestSize.Start();
                        OriginalDestSize.Wait();
                        long OrgSize = OriginalDestSize.Result;


                        // Copying the directory
                        Task task2 = new Task(() => duf.CopyDirectories(s, newDestPath));
                        task2.Start();


                        ////Updating of the percentage
                        //while (!task2.IsCompleted)
                        //{
                        //    //Console.Write("Original Destination Size is {0}", OrgSize);
                        //    long test = DirectorySize(newDestPath);
                        //    //Console.WriteLine("New Size is {0}", test);
                        //    // Console.WriteLine("It is {0.##}% Complete", (float)test / OrgSize);
                        //    Thread.Sleep(5000);
                        //}
                        task2.Wait();
                        Console.WriteLine();


                        if (first)
                        {
                            //UpdateDataElement(currDe, Directory.GetCreationTime(s));
                            UpdateDataElement(currDe, fg.NewestDT);
                            StaticDirectoryListings.UpdateFile(dictionaryList);
                            CheckSubDirectories(newDestPath, dataset);
                            
                          
                        }


                        first = false; // This is to ensure that the zip will be updated once only
                    }


                }


            }
        }

        private void UpdateToFTP(string zipFile)
        {
            Console.WriteLine("Writing to FTP ZipFIle");
            SFTPAccess sa = new SFTPAccess();
            UploadSFTP us = new UploadSFTP();
            string dataset = us.GetDatasetName(zipFile);
            sa.AddFile(zipFile, dataset);
        }

        /// <summary>
        /// Will check directories to see if it has subdirectories of:
        /// Address Data
        /// Supression Data
        /// or whether the subdirectory has Cd Images
        /// </summary>
        /// <param name="path">Path used</param>
        /// <param name="dataset">Dataset being used</param>
        private void CheckSubDirectories(string originalPath, string dataset)
        {

            String folderName = Path.GetFileName(originalPath);
            FolderScan fs = new FolderScan();
            FileStringFormatting fsf = new FileStringFormatting();
            Console.WriteLine("CSD: Zipping up {0} which is of Dataset: {1}", originalPath, dataset);
            if (fs.ContainsSubFolder(originalPath, "Cd Images"))
            {
                string path = originalPath;
                // Moving to new path of CD Images
                path = Path.Combine(path, "CD Images");

                List<string> subDirectory = fs.GetAddressDataSubdirectory(path);

                if (subDirectory.Count > 0)
                {
                    foreach (string sub in subDirectory)
                    {
                        string tempPath = Path.Combine(path, sub);
                        //string zipDestinationString = Path.Combine(zipFileLocation, dataset + " " + folderName + " " + sub + ".zip");
                        string zipDestination = Path.Combine(zipFileLocation, dataset + " " + folderName + " " + sub + ".zip");
                        ZipFile(tempPath, zipDestination);

                    }
                }
            }

            if (fs.ContainsSubFolder(originalPath, "Cd Image"))
            {
                string path = originalPath;
                path = Path.Combine(path, "CD Image");
                if (fs.CheckHasSetupFile(path))
                {
                    string zipDestination = Path.Combine(zipFileLocation, dataset + " " + folderName + ".zip");
                    ZipFile(path, zipDestination);
                }
            }
            
            if (fs.ContainsSubFolder(originalPath, "SERP"))
            {
                string path = originalPath;
                path = Path.Combine(path, "SERP");
                if (fs.CheckHasSetupFile(path))
                {
                    
                    string zipDestination = Path.Combine(zipFileLocation, dataset + " " + folderName + ".zip");
                    ZipFile(path, zipDestination);
                }
            }


            // Should probably have a if statement for if the subdirectory is
            // address data, supression data,right away
        }



        /// <summary>
        /// Will zip up the file and encrypt it
        /// </summary>
        /// <param name="destPath">The path to be zipped and </param>
        private void ZipFile(string destPath)
        {
            String newZip = Path.GetDirectoryName(destPath) + "\\" + Path.GetFileName(destPath) + ".zip";
            ZipFile(destPath, newZip);
        }

        /// <summary>
        /// Will zip up the file to encrypt and zip it
        /// zipPath will be e.g. C:\test\File.zip
        /// </summary>
        /// <param name="destPath">path to be zipped</param>
        /// <param name="zipPath">path where the zip would be located. </param>
        private void ZipFile(string destPath, string zipPath)
        {
            Console.WriteLine("\nZipFile:\nZipPath is {0}", zipPath);
            Console.WriteLine("Destpath is {0}", destPath);
            ZipEncryption ze = new ZipEncryption();
            FileStringFormatting fsf = new FileStringFormatting();
            String randomGen = ze.EncryptionPasswordGenerator(8);
            String newZip = zipPath;
            string fileName = Path.GetFileNameWithoutExtension(zipPath);
            List<string> splits = fsf.ReturnStringSplit(fileName);
            string dirPath = Path.GetDirectoryName(zipPath);

            string year = splits.ElementAt(1);
            string dataSet = splits.ElementAt(0);
            string month = fsf.GetMonth(splits.ElementAt(2));
            string extraElement = fsf.ReturnExtraElements(splits);
            string finalFileName = (dataSet + " " + month + " " + year + " " + extraElement).Trim();

            string addInformation = CheckSerp(destPath);
            if (addInformation!="")
            {
                finalFileName = finalFileName + " " + addInformation;
            }

            newZip = Path.Combine(dirPath, finalFileName + ".zip");

            FileInfo fi = new FileInfo(newZip);

            for (int i = 1; fi.Exists; i++)
            {
                newZip = Path.Combine(Path.GetDirectoryName(fi.FullName), Path.GetFileNameWithoutExtension(fi.FullName) + "_" + i + ".zip");
                fi = new FileInfo(newZip);
            }

            // uncomment this
            ze.ZipWithEncryption(destPath, randomGen, newZip);

            Console.WriteLine("newZip is {0}", newZip);

            using (StreamWriter sw = new StreamWriter("FileList.log", true))
            {
                sw.WriteLine(newZip);
            }
            writeToLog(newZip, randomGen);

            //Updating to FTP
            UpdateToFTP(newZip);
        }


        private string CheckSerp(string destPath)
        {
            if (Path.GetFileName(destPath).Equals("SERP"))
                return "SERP";
            else
                return "";

        }

        private void writeToLog(string newZip, string randomGen)
        {
            FileWriter fw = new FileWriter();
            fw.AppendToFile("Created at" + DateTime.Now.ToShortDateString());
            fw.AppendToFile(fw.CreateDetails(newZip, randomGen));
        }

        private void UpdateDataElement(DataElement currD, DateTime dateCreated)
        {
            //Console.WriteLine("Updating Data Element so that the lastUpdated will be teh date of the dataset {0}", dateCreated.ToString());
            currD.LastModified = dateCreated;
        }

        private void ShowDatasetsList()
        {
            Console.WriteLine("Will show datasets");
            foreach (var dict in StaticDirectoryListings.ReadJson())
            {
                Console.WriteLine("\nElement Key:{0}", dict.Key);
                Console.WriteLine(dict.Value);
            }
        }

        private String[] SplitInput(String command)
        {
            String[] Inputs = new String[2];
            command = command.Trim();

            if (command.Equals("q"))
            {
                Inputs[0] = "q";
                Inputs[1] = "";
                return Inputs;
            }

            if (command.Equals("s"))
            {
                Inputs[0] = "s";
                Inputs[1] = "";
                return Inputs;
            }

            if (!command.Equals("") && command.Contains(" "))
            {
                Inputs = command.Split(new char[] { ' ' }, 2);
            }
            else
            {
                //Inputs[0] = "";
                Inputs[0] = command;
                Inputs[1] = "";
            }

            return Inputs;
        }

        public DirectoryUpdate()
        {
            duf = new DirectoryUpdateFile();
        }
    }


}
