using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace com.qas.sambo.directoryupdate.SampleTest
{
    class SFTPExampleClass
    {
        public static void Main()
        {
            host = @"secureftp.experian.com.au";
            port = 22;
            username = @"PIF0014";
            //password = @"Q$sAdm2n";
            password = "Q$SadM2n";
            remoteDirectory = @"\Uploads";

            Console.WriteLine("Testing the SFTP Client");
            try
            {
                using (SftpClient sftp = new SftpClient(host, port, username, password))
                {
                    sftp.Connect();
                    Console.WriteLine("Connected succesfully");

                    Console.WriteLine("Listing Directory");

                    var files = sftp.ListDirectory(remoteDirectory);
                    foreach (var file in files)
                    {
                        Console.WriteLine(file.FullName);
                    }
                    //sftp.ChangeDirectory(@"\data\AUE");

                    //sftp.CreateDirectory("Uploads");
                    sftp.Disconnect();
                }
            }
               
            catch(SftpPermissionDeniedException sde)
            {
                Console.WriteLine("Permission is denied at writing. There might be an existing folder or check for permissions.");
                Console.WriteLine("Error is: {0}", sde.ToString());
            }
            catch (Exception ae)
            {
                Console.WriteLine("Exception e is {0}", ae.ToString());
            }
            finally
            {
                Console.WriteLine("\n\nClosing the application.");
            }
        }

        public static string host { get; set; }

        public static int port { get; set; }

        public static string username { get; set; }

        public static string password { get; set; }

        public static string remoteDirectory { get; set; }
    }
}
