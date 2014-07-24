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
            creationDirectoryPath = @"/Uploads";
            localPath = @"C:\MyFiles\Temp\TestUploads\";
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
            try
            {
                using (SftpClient sftp = new SftpClient(host, port, username, password))
                {
                    sftp.Connect();
                    Console.WriteLine("Downloading from directory: {0}", creationDirectoryPath + dir);
                    sftp.ChangeDirectory(creationDirectoryPath);
                    var filesList = sftp.ListDirectory(dir);
                    foreach (var file in filesList)
                    {
                        //int lastIndex = file.FullName.ToString().LastIndexOf('/')+1;
                        //string lastString = file.FullName.ToString().Substring(lastIndex);
                        string lastString = Path.GetFileName(file.FullName);
                        Console.WriteLine("Local path is {0}", localPath + lastString);
                        //using (var localFile = File.OpenWrite(localPath+file.FullName))
                        //{
                        //    Console.WriteLine("File found: {0}", file.FullName);
                        //    sftp.DownloadFile(dir,localFile );
                        //}


                    }
                    //using (var fs = new FileStream(localPath                    sftp.DownloadFile(dir,)
                    sftp.Disconnect();
                }
            }
            catch (SftpPermissionDeniedException spde)
            {
                Console.WriteLine("Denied. Check Permission and make sure it's not already created");
            }
            catch (System.IO.IOException io)
            {
                Console.WriteLine("AN error occured when trying to copy the files. Check if it cannot create files because it is already there. ");
            }
            catch (SftpPathNotFoundException spnfe)
            {
                Console.WriteLine("Can you check if the path specified is correct?");
            }
        }

        public string host { get; set; }

        public int port { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string remoteDirectory { get; set; }

        public string creationDirectoryPath { get; set; }

        public string localPath { get; set; }
    }
}

