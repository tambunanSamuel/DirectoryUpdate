using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace com.qas.sambo.directoryupdate.Utils
{
    class DirectoryCopy
    {
        public DirectoryCopy()
        {

        }

        public void FileCopy(string originalPath, string destPath)
        {
            FileStream op = new FileStream(originalPath,FileMode.Open);
            
            using (FileStream destStream = File.Open(destPath,FileMode.Create))
            {
                int byteValue;
                while((byteValue = op.ReadByte()) != -1)
                    destStream.WriteByte((byte)byteValue);
            }
        }

        public IAsyncResult CreateRandomFile(string destPath, int bytesUsed)
        {
       
            byte[] dataArray = new byte[bytesUsed];
            new Random().NextBytes(dataArray);

          
                using (FileStream destStream = new FileStream(destPath, FileMode.Create))
                {
                    for (int i = 0; i < dataArray.Length; i++)
                    {
                        destStream.WriteByte(dataArray[i]);

                        //IAsyncResult++;
                    }
                }
        
                

            //return ias;
                return null;
        }
    }

    class MyAsyncResult :IAsyncResult
    {
        public ulong statusNumber { get; set; }
        
        public MyAsyncResult(AsyncCallback asyncCallback, object state)
        {
        }

        public object AsyncState
        {
            get { throw new NotImplementedException(); }
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public bool CompletedSynchronously
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsCompleted
        {
            get { throw new NotImplementedException(); }
        }
    }
}
