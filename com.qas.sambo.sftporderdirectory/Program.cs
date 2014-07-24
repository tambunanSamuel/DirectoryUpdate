using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.qas.sambo.sftporderdirectory.Utils;

namespace com.qas.sambo.sftporderdirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pp = new Program();
            pp.RunProgram();
        }

        public void RunProgram()
        {
            Console.WriteLine("==== Please Specify either c or d to either create a directory or download and upload to the N Drive ====");
            Console.WriteLine("To exit, type q\n");
            Console.WriteLine("Insert Command:");

            var command = Console.ReadLine().ToLower();

            String[] Inputs = SplitInput(command);

            while (!Inputs[0].Equals("q"))
            {
                
                switch (Inputs[0])
                {
                    case "c":
                        CreateDirectory(Inputs[1]);
                        break;
                    case "d":
                        DownloadDirectory(Inputs[1]);
                        break;
                    default:
                        Console.WriteLine("An example of a command is \"C HE-43SF\"");
                        Console.WriteLine("Please specify a command with: q, c ARGS, d ARGS");
                        break;
                }

                Console.WriteLine("Insert Command:");
                Inputs = SplitInput(Console.ReadLine().ToLower());
            }
        }

        /// <summary>
        /// Will create a directory path in the FTP 
        /// </summary>
        /// <param name="directory"></param>
        private void CreateDirectory(string directory)
        {
            SFTPConnector sc = new SFTPConnector();
            sc.CreateDirectory(directory);
        }

        /// <summary>
        /// Will download the directory with the end path of directory
        /// and then upload it to the N Drive
        /// </summary>
        /// <param name="directory"></param>
        private void DownloadDirectory(string directory)
        {
            SFTPConnector sc = new SFTPConnector();
            sc.UploadFilesToFolder(directory);
            //sc.createfolder(directory);
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
    }
}
