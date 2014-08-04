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


namespace com.qas.sambo.directoryupdate
{
    class DirectoryUpdate
    {
        DirectoryUpdateFile duf;
        public static void Main()
        {
            StaticDirectoryListings.init();
            DirectoryUpdate du = new DirectoryUpdate();
            du.RunProgram();

        }

        /// <summary>
        /// To run the Program
        /// </summary>
        public void RunProgram()
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
                        //Task1
                        //Task task1 = new Task(new Action(DownloadDatasetsList));
                        //task1.Start();
                        //Console.WriteLine("Downloading\n");
                        //while (!task1.IsCompleted)
                        //{
                        //    Console.Write(".");
                        //    Thread.Sleep(2000);
                        //}

                        Console.WriteLine("Finished Downloading");
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
                            //      testc(Inputs[1]);
                            //  testc(@"\\Product\product\World Data\NZL\v4\2014 06-June-Q");
                        }

                        break;
                    default:

                        break;
                }

                Console.WriteLine("Insert Command:");
                command = Console.ReadLine().ToLower();
                Inputs = SplitInput(command);
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

            foreach (var dicList in StaticDirectoryListings.DeList)
            {
                //Console.WriteLine("Key is {0}", dicList.Key);
                DataElement currDe = dicList.Value;
                Task<List<string>> task1 = new Task<List<string>>(() => duf.ReturnDirectories(currDe.SourcePath, currDe.LastModified));
                task1.Start();
                Console.WriteLine("Reading directories and files");
                while (!task1.IsCompleted)
                {
                    Console.Write(".");
                    Thread.Sleep(2000);
                }
                task1.Wait();
                Console.WriteLine();
                List<string> directoriesList = task1.Result;
                foreach (var s in directoriesList)
                {
                    foreach (var destPath in currDe.DestinationPath)
                    {
                        var newDestPath = destPath + "\\" + Path.GetFileName(s);
                        Console.WriteLine("NewDestPath is {0}", newDestPath);
                        
                        Console.WriteLine("Copying {0} into {1}", s, newDestPath);

                        //Getting size of directory
                        Task<long> OriginalDestSize = new Task<long>(() => DirectorySize(s));
                        OriginalDestSize.Start();
                        OriginalDestSize.Wait();
                        long OrgSize = OriginalDestSize.Result;
                        // Copying the directory
                        Task task2 = new Task(() => duf.CopyDirectories(s, newDestPath));
                        task2.Start();

                        //Task<long> newDestSize = new Task<long>(() => DirectorySize(newDestPath));
                        //newDestSize.Start();

                        while (!task2.IsCompleted)
                        {
                            Console.Write("Original Destination Size is {0}", OrgSize);
                            //Task<long> NewDirectorySize = new Task<long>(() => DirectorySize(newDestPath));
                            //NewDirectorySize.Start();
                            //NewDirectorySize.Wait();

                            //long test = NewDirectorySize.Result;
                            //NewDirectorySize.Dispose();
                            long test = DirectorySize(newDestPath);
                            Console.WriteLine("New Size is {0}", test);
                            Console.WriteLine("It is {0:##}% Complete", (float)test / OrgSize);
                            Thread.Sleep(5000);
                        }

                        task2.Wait();
                        Console.WriteLine();
                        UpdateDataElement(currDe, Directory.GetCreationTime(s));
                        ZipFile(newDestPath);
                    }

                    // 1) Download the dataset to the correct path 

                }


            }
        }

        /// <summary>
        /// Will zip up the file and encrypt it
        /// </summary>
        /// <param name="destPath">The path to be zipped and </param>
        private void ZipFile(string destPath)
        {
            ZipEncryption ze = new ZipEncryption();
            Console.WriteLine("Will zip up {0}", destPath);

            String randomGen = ze.EncryptionPasswordGenerator(8);
            String newZip = Path.GetDirectoryName(destPath) + "\\" + Path.GetFileName(destPath) + ".zip";
            ze.ZipWithEncryption(destPath,randomGen,newZip);
            //Console.WriteLine("Path is {0}", Path.GetDirectoryName(destPath)+"\\"+Path.GetFileName(destPath)+".zip");
            ///throw new NotImplementedException();
        }

        private void UpdateDataElement(DataElement currD, DateTime dateCreated)
        {
            Console.WriteLine("Updating Data Element so that the lastUpdated will be teh date of the dataset {0}", dateCreated.ToString());
            currD.LastModified = dateCreated;
            //throw new NotImplementedException();
        }

        private void ShowDatasetsList()
        {
            Console.WriteLine("Will show datasets");
            foreach (var dict in StaticDirectoryListings.DeList)
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
