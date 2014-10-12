using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.CSharpLearning
{
    class MyCopyFileAPI
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName,
            CopyProgressRoutine lpProgressRoutine, IntPtr lpData, ref Int32 pbCancel, CopyFileFlags dwCopyFlags);


        [Flags]
        enum CopyFileFlags : uint
        {
            COPY_FILE_FAIL_IF_EXISTS = 0x00000001,
            COPY_FILE_RESTARTABLE = 0x00000002
        }

        enum CopyProgressResult
        {
            PROGRESS_CANCEL = 1,
            PROGRESS_CONTINUE = 0,
            PROGRESS_QUIET = 3,
            PROGRESS_STOP = 2
        }

        delegate CopyProgressResult CopyProgressRoutine(
            long totalFileSize,
            long totalBytesTransferred,
            long streamSize,
            long streamBytesTransferrred,
            uint streamNumber,
            CallBackReason callBack,
            IntPtr hSourceFile,
            IntPtr hDestinationFile,
            IntPtr lpData
            );

        [Flags]
        enum CallBackReason
        {
            CALLBACK_CHUNK_FINISHED = 0x00000000,
            CALLBACK_STREAM_SWITCH = 0x00000001
        }

    }
}
