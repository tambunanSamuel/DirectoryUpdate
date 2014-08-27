using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace com.qas.sambo.directoryupdate.Utils
{
    class UploadSFTP
    {
        public UploadSFTP()
        { }

        public string GetDatasetName(string zipPath)
        {
            string fileName = Path.GetFileName(zipPath);
            string[] s = fileName.Split(' ');
            return s[0];
        }

    }


}
