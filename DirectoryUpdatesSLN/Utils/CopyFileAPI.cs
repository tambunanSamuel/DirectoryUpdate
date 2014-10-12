using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace com.qas.sambo.directoryupdate
{
    [Flags]
    public enum CopyFileOptions
    {
        None = 0,
        AllowDecryptedDestination = 0x00000008,
        CopySumlink = 0x00000800,
        FailIfExists = 0x00000001,
        NoBuffering = 0x00001000,
        OpenSourceForWrite = 0x00000004,
        Restartable = 0x00000002
    }

    public enum CopyCallbackReason
    {
        ChunkFinished = 0,
        SreamSwitch = 1
    }

    public enum ProgressCallbackResult
    {
        Continue = 0,
        Cancel = 1,
        Stop = 2,
        Quiet = 3
    }


    public class CopyProgressArgs
    {
        public readonly Int64 TotalFileSize;
        public readonly Int64 TotalBytesTransferred;
        public readonly Int64 StreamSize;
        public readonly Int64 StreamBytesTransferred;
        public readonly Int32 StreamNumber;
        public readonly Int32 CallbackREason;
        public readonly IntPtr SourceHandle;
        public readonly IntPtr DestinationHandle;
        public readonly Object UserData;
        public ProgressCallbackResult Result { get; set; }
        public CopyProgressArgs(Int64 fsize, Int64 xferBytes, Int64 strmSize, Int64 strmXferBytes, Int32 strmNum, Int32 reason, IntPtr srcHandle, IntPtr destHandle, Object uData)
        {
            TotalFileSize = fsize;
            TotalBytesTransferred = xferBytes;
            StreamSize = strmSize;
            StreamBytesTransferred = strmXferBytes;
            StreamNumber = strmNum;
            CallbackREason = reason;
            SourceHandle = srcHandle;
            DestinationHandle = destHandle;
            UserData = uData;
        }
    }

    public delegate void CopyProgressDelegate(CopyProgressArgs cpa);


    class CopyFileAPI
    {
        public delegate Int32 APICopyProgressRoutine(
        Int64 TotalFileSize,
        Int64 TotalBytesTransferred,
        Int64 StreamSize,
        Int64 StreamBytesTransferred,
        Int32 StreamNumber,
        Int32 CallbackReason,
        IntPtr SourceHandle,
        IntPtr DestinationHandle,
        Object UserData
        );

        struct CopyFileResult
        {
            public readonly bool ReturnValue;
            public readonly int LastError;
            public CopyFileResult(bool rslt, int err)
            {
                ReturnValue = rslt;
                LastError = err;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CopyFileEx(
            string glpExistingFileName,
            string lpNewFileName,
            APICopyProgressRoutine lpProgressRoutine,
            Object lpData,
            ref Int32 lpCancel,
            CopyFileOptions dwCopyFlags);

        static private CopyFileResult CopyFileInternal(
            string sourceFilename,
            string destinationFilename,
            CopyProgressDelegate progressHandler,
            Object userData,
            CopyFileOptions copyOptions,
            CancellationToken cancelToken)
        {
            // On error, throw IOException with the value from Marhsal.GetLastWin32Error
            CopyProgressDelegate handler = progressHandler;
            int cancelFlag = 0;
            APICopyProgressRoutine callback;

            if (handler == null)
                callback = null;
            else
            {
                callback = new APICopyProgressRoutine((tfSize, xferBytes, strmSize, strmXferBytes,
                    strNum, cbReason, srcHandle, dstHandle, udata) =>
                    {
                        var args = new CopyProgressArgs(tfSize, xferBytes, strmSize, strmXferBytes,
                            strNum, cbReason, srcHandle, dstHandle, udata);
                        handler(args);
                        return (Int32)args.Result;
                    }
                    );
            }

            if (cancelToken.CanBeCanceled)
            {
                cancelToken.Register(() => { cancelFlag = 1; });
            }
            bool rslt = CopyFileEx(
                sourceFilename,
                destinationFilename,
                callback,
                userData,
                ref cancelFlag,
                copyOptions);

            int err = 0;
            if (!rslt)
            {
                err = Marshal.GetLastWin32Error();
            }
            return new CopyFileResult(rslt, err);
        }

        public static void CopyFile(
            string sourceFilename,
            string destinationFilename,
            CopyProgressDelegate progressHandler,
            Object userData,
            CopyFileOptions copyOptions,
            CancellationToken cancelToken)
        {
            var rslt = CopyFileInternal(
                sourceFilename,
                destinationFilename,
                progressHandler,
                userData,
                copyOptions,
                cancelToken);

            if (!rslt.ReturnValue)
            {
                throw new IOException(string.Format("Error copying file. GetLastError returns {0}.", rslt.LastError));
            }
        }

        public static void CopyFile(string sourceFilename,
            string destinationFilename)
        {
            CopyFile(sourceFilename, destinationFilename, null, null, CopyFileOptions.None, CancellationToken.None);
        }

        public static void CopyFileProgress(string sourceFileName, string destFileName)
        {
            
            //var cts = new CancellationToken();
            //CopyFile(sourceFileName, destFileName, null, null, CopyFileOptions.None, CancellationToken.None);
            CopyFile(sourceFileName, destFileName, null, null, CopyFileOptions.None, CancellationToken.None);
        }

        static void MyCopyProgress(CopyProgressArgs e)
        {
            //Console.Write("\r{0:N0}/{1:N0} ({2:P2})", e.TotalBytesTransferred, e.TotalFileSize, (double)e.TotalBytesTransferred / e.TotalFileSize);
        }
        public static void Main()
        {
            Console.WriteLine("Testing the Copy File Method");
            CopyFileAPI.CopyFileProgress(@"C:\MyFiles\Programming\Testing\RandomFile.asd", @"C:\MyFiles\Programming\Testing\RandomFile2.asd");
            //CopyFileAPI.CopyFile(@"C:\MyFiles\Programming\Testing\RandomFile.asd", @"C:\MyFiles\Programming\Testing\RandomFile2.asd");
            
        }
    }




}
