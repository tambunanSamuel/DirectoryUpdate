using com.qas.sambo.directoryupdate.data;
using com.qas.sambo.directoryupdate.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


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

                switch (command)
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
                    default:
                        
                        break;
                }

                Console.WriteLine("Insert Command:");
                command = Console.ReadLine().ToLower();
            }

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
            Console.WriteLine("Downloading datasets");
            foreach (var dicList in StaticDirectoryListings.DeList)
            {
                //Console.WriteLine("Key is {0}", dicList.Key);
                DataElement currDe= dicList.Value;
                foreach (var s in duf.ReturnDirectories(currDe.SourcePath, currDe.LastModified))
                {
                    //Console.WriteLine("Copying Directory {0} ===", s);
                    foreach (var destPath in currDe.DestinationPath)
                    {
                        var newDestPath = destPath + Path.GetFileName(s);
                        Console.WriteLine("NewDestPath is {0}", newDestPath);
                        //duf.CopyDirectories(s, destPath);
                        Console.WriteLine("Copying {0} into {1}", s, destPath);
                        //Task task2 = new Task(() => duf.CopyDirectories(s, destPath));
                        ////Task task1 = new Task(() => UpdateDataElement(currDe, Directory.GetCreationTime(s)));
                        ////task1.Start();
                        //task2.Start();
                        //while (!task2.IsCompleted)
                        //{
                        //    Console.Write(".");
                        //    Thread.Sleep(2000);
                        //}
                            
                        //task2.Wait();
                        //Console.WriteLine();
                        //UpdateDataElement(currDe, Directory.GetCreationTime(s));
                        ZipFile(destPath);
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
            Console.WriteLine("Will zip up {0}", destPath);
            ///throw new NotImplementedException();
        }

        private void UpdateDataElement(DataElement currD, DateTime dateCreated)
        {
            Console.WriteLine("Updating Data Element so that the lastUpdated will be teh date of the dataset {0}",dateCreated.ToString());
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
                Inputs[0] = "";
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
