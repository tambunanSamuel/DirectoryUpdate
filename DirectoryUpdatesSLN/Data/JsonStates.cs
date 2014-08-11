using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///This class is used to represent a Json state for the dataset update value
namespace com.qas.sambo.directoryupdate.Data
{
    class JsonStates
    {
        public string Dataset { get; set; }
        public string ServerLocation { get; set; }
        public List<string> DownloadLocation { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
