using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.directoryupdate
{
    class CopyFileExTest
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName,
           CopyProgressRoutine lpProgressRoutine, IntPtr lpData, ref Int32 pbCancel,
           CopyFileFlags dwCopyFlags);

        delegate CopyProgressResult CopyProgressRoutine(
        long TotalFileSize,
        long TotalBytesTransferred,
        long StreamSize,
        long StreamBytesTransferred,
        uint dwStreamNumber,
        CopyProgressCallbackReason dwCallbackReason,
        IntPtr hSourceFile,
        IntPtr hDestinationFile,
        IntPtr lpData);

        int pbCancel;

        enum CopyProgressResult : uint
        {
            PROGRESS_CONTINUE = 0,
            PROGRESS_CANCEL = 1,
            PROGRESS_STOP = 2,
            PROGRESS_QUIET = 3
        }

        enum CopyProgressCallbackReason : uint
        {
            CALLBACK_CHUNK_FINISHED = 0x00000000,
            CALLBACK_STREAM_SWITCH = 0x00000001
        }

        [Flags]
        enum CopyFileFlags : uint
        {
            COPY_FILE_FAIL_IF_EXISTS = 0x00000001,
            COPY_FILE_RESTARTABLE = 0x00000002,
            COPY_FILE_OPEN_SOURCE_FOR_WRITE = 0x00000004,
            COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 0x00000008
        }

        public void XCopy(string oldFile, string newFile)
        {
            Console.WriteLine("\nCopying into {0}", newFile);
            CopyFileEx(oldFile, newFile, new CopyProgressRoutine(this.CopyProgressHandler), IntPtr.Zero, ref pbCancel, CopyFileFlags.COPY_FILE_RESTARTABLE);
        }

        private CopyProgressResult CopyProgressHandler(long total, long transferred, long streamSize, long StreamByteTrans, uint dwStreamNumber, CopyProgressCallbackReason reason, IntPtr hSourceFile, IntPtr hDestinationFile, IntPtr lpData)
        {
            Console.Write("\rProgress is {0:P2}", (double)transferred / total);
            return CopyProgressResult.PROGRESS_CONTINUE;
        }

        public void CopyDirectory(string dirValue, string newDir)
        {

            Console.WriteLine("=== Copying From {0} into {1} ===", dirValue, newDir);
            var dirs = Directory.EnumerateDirectories(dirValue, "*", SearchOption.AllDirectories);
            var fileList = Directory.EnumerateFiles(dirValue, "*", SearchOption.AllDirectories);

            foreach (var d in dirs)
            {
                var dirRep = d.Replace(dirValue, newDir);
                //Console.WriteLine("Creating Directory {0}", dirRep);
                Directory.CreateDirectory(dirRep);
            }
            int totalFileNumber = fileList.Count();
            int fileCopied = 0;
            foreach (var f in fileList)
            {

                var dirRep = f.Replace(dirValue, newDir);
                //Console.WriteLine("Files {0}", dirRep);
                Console.WriteLine("[{0}/{1}]\n", ++fileCopied, totalFileNumber);
                Console.WriteLine("Copying with progress from {0} to {1}", dirValue, newDir);
                XCopy(f, dirRep);
            }
        }

        public static void Main()
        {
            Console.WriteLine("Testing CopyFileExTest");
            CopyFileExTest cfet = new CopyFileExTest();
            cfet.XCopy(@"C:\MyFiles\Programming\Testing\RandomFile.asd", @"C:\MyFiles\Programming\Testing\RandomFile2.asd");
        }

    }
}
