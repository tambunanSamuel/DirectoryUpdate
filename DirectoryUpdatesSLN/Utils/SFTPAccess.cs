using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;
using System.Threading;


namespace com.qas.sambo.directoryupdate.Utils
{
    class SFTPAccess
        
    {
        private string userName = "PIF0013";
        private string password = "Q$sAdm2n";
        private int port = 22;
        private string host = "secureftp.experian.com.au";
        private SftpClient sftpInstance;
        private string homeDirectory = @"/Orders/Temp/";
        

        public SFTPAccess()
        {
            //host = "192.168.2.145";
            //userName = "samuel";
            //password = "sammy";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFilePath">File Path of the ZIP file</param>
        /// <param name="dataSetName">Dataset Used</param>
        public void AddFile(string localFilePath, string dataSetName)
        {
            Console.WriteLine("Uploading: {0}", localFilePath);
            string fileName = Path.GetFileName(localFilePath);
            try
            {
                using (sftpInstance = new SftpClient(host, port, userName, password))
                {
                    sftpInstance.Connect();
                    sftpInstance.ChangeDirectory(homeDirectory);
                    sftpInstance.ChangeDirectory(dataSetName);
                    long fileSize = new FileInfo(localFilePath).Length;
                    using (FileStream file = new FileStream(localFilePath, FileMode.Open))
                    {
                        //synchronous
                       // sftpInstance.UploadFile(file, fileName);
                        //var sftpAsynch = async as Renci.SshNet.Sftp.SftpDownloadAsyncResult;
                       var async = sftpInstance.BeginUploadFile(file, fileName);
                       var sftpAsync = async as Renci.SshNet.Sftp.SftpUploadAsyncResult;
                        while (!sftpAsync.IsCompleted)
                        {
                            int cursorPos = Console.CursorTop;
                            Console.WriteLine("Uploaded {0} out of {1}: {2:0.00}%", sftpAsync.UploadedBytes, fileSize, (float)sftpAsync.UploadedBytes/fileSize*100);
                            Console.SetCursorPosition(0,cursorPos);
                            Thread.Sleep(500);
                        }
                       
                    }
                    sftpInstance.Disconnect();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Check to see if File is valid and the Dataset Path folder is available");
                Console.WriteLine("Exception is {0}", e);
            }
        }

        
        public static void Main()
        {
            SFTPAccess sf = new SFTPAccess();
            sf.AddFile("test", "test");
        }

        public void ChangeToDirectory(string directory)
        {
            homeDirectory = directory;
        }


    }
}
