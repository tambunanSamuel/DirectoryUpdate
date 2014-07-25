using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.IO;

namespace com.qas.sambo.sftporderdirectory.Utils
{
    class SFTPConnector
    {
        public SFTPConnector()
        {

            // Creation of the hosts
            host = @"secureftp.experian.com.au";
            port = 22;
            username = @"PIF0014";
            password = "Q$SadM2n";
            remoteDirectory = ".";
            creationDirectoryPath = @"/Uploads/";
            localPath = @"C:\MyFiles\Temp\TestUploads\";
            currentPath = "";
            ftpDir = "";
        }



        /// <summary>
        /// Create directory with the above descriptions
        /// </summary>
        /// <param name="dir"></param>
        public void CreateDirectory(string dir)
        {
            try
            {
                using (SftpClient sftp = new SftpClient(host, port, username, password))
                {
                    sftp.Connect();
                    Console.WriteLine("Creating a directory: {0}", dir.ToUpper());
                    sftp.ChangeDirectory(creationDirectoryPath);
                    sftp.CreateDirectory(dir);
                    sftp.Disconnect();
                }
            }
            catch (SftpPermissionDeniedException spde)
            {
                Console.WriteLine("Denied. Check Permission and make sure it's not already created");
            }

        }
        /// <summary>
        /// Creates a Folder in the N Drive and then will download all the files from the sFTP
        /// with that directory into it. 
        /// For example: if dir = HE-5500
        /// It will download the files from the sFTP with /Uploads/HE-5500
        /// and upload it to
        /// N:\Uploads\HE-5500
        /// </summary>
        /// <param name="dir"></param>
        public void UploadFilesToFolder(string dir)
        {
            ftpDir = creationDirectoryPath + dir;
            dir = localPath + dir;

            try
            {
                using (SftpClient sftp = new SftpClient(host, port, username, password))
                {
                    sftp.Connect();
                    Console.WriteLine("Downloading from directory: {0}", creationDirectoryPath + dir);
                    //sftp.ChangeDirectory(creationDirectoryPath);
                    Console.WriteLine("Dir {0}, sftp {1}", ftpDir, sftp);
                    DownloadRecursive(ftpDir, sftp, localPath);
                    sftp.Disconnect();
                    Console.WriteLine();
                }
            }
            catch (SftpPermissionDeniedException spde)
            {
                Console.WriteLine("Denied. Check Permission and make sure it's not already created");
            }
            catch (System.IO.IOException io)
            {
                Console.WriteLine("AN error occured when trying to copy the files. Check if it cannot create files because it is already there. ");
                Console.WriteLine(io.ToString());
            }
            catch (SftpPathNotFoundException spnfe)
            {
                Console.WriteLine("Error with the sfTP path");
                Console.WriteLine("Can you check if the path specified is correct?: {0}",spnfe.ToString());
            }
        }


        /// <summary>
        /// dir the directory to download
        /// </summary>
        /// <param name="dir"></param>
        private void DownloadRecursive(string dir, SftpClient sftp, string localDir)
        {
            Console.WriteLine("Dir {0}", dir);
            var filesList = sftp.ListDirectory(dir);
            foreach (var file in filesList)
            {
                if (!file.IsDirectory)
                {
                    Console.WriteLine("==== New file would be {0}", localDir+Path.GetFileName(file.FullName));
                    //using (var localFile = new FileStream(currentPath + lastString, FileMode.Create))
                    //{

                    //    sftp.DownloadFile(file.FullName, localFile);

                    //}
                    Console.WriteLine("File {0} is a file and fullname is {1}", file, Path.GetFileName(file.FullName));
                }
                else
                {
                    string CreationDirectory = localDir + Path.GetFileName(file.FullName) + "\\";
                    createfolder(CreationDirectory);
                    Console.WriteLine("New Directory to check {0}, and local {1}\n", file.FullName, CreationDirectory);
                    DownloadRecursive(file.FullName, sftp,CreationDirectory);
                    Console.WriteLine("File {0} is a Directory", file);
                }

            }

            //createfolder(dir);
            //Path.GetFileName(dir);
            //var filesList = sftp.ListDirectory(dir);
            //foreach (var file in filesList)
            //{
            //    string lastString = Path.GetFileName(file.FullName);
            //    Console.WriteLine("Downloaded: {0} to {1}", file.FullName, currentPath + lastString);
            //    Console.WriteLine("Path name is {0}", Path.GetDirectoryName(file.FullName));

            //    if (!file.IsDirectory)
            //    {
            //        using (var localFile = new FileStream(currentPath + lastString, FileMode.Create))
            //        {

            //            sftp.DownloadFile(file.FullName, localFile);

            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("NEw Dir should be {0}", dir + file.FullName);
            //    }

            //}
            ////using (var fs = new FileStream(localPath                    sftp.DownloadFile(dir,)
        }

        public void createfolder(string dir)
        {

            Console.WriteLine("Creating Folder at: {0}", dir);

            System.IO.Directory.CreateDirectory(dir);

        }

        public string host { get; set; }

        public int port { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string remoteDirectory { get; set; }

        public string creationDirectoryPath { get; set; }

        public string localPath { get; set; }

        public string currentPath { get; set; }

        public string ftpDir { get; set; }
    }
}

