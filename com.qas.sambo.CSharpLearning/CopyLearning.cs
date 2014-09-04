using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.CSharpLearning
{
    class CopyLearning
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool CopyFileEx();

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool CopyFile(string existingFileName, string newFilename, bool fails);

        public static async Task CreateRandomFile(string pathway, int size, IProgress<FileCopiedClass> prog)
        {
            byte[] fileSize = new byte[size];
            new Random().NextBytes(fileSize);
            FileCopiedClass fcc = new FileCopiedClass();
            fcc.actualSize = (ulong) size;
            await Task.Run(() =>
               {
                   using (FileStream fs = File.Create(pathway, 4096))
                   {

                       for (int i = 0; i < size; i++)
                       {
                           fs.WriteByte(fileSize[i]);
                           //if (i % 4000 == 0)
                           {
                               fcc.progress = (ulong)i;
                               Console.WriteLine("Copied {0} from {1} , percentage {2:00}%", i, size, (float)i / size*100);
                               prog.Report(fcc);
                           }
                       }

                   }

               }
           );
        }

        public static void p_ProgressChanged(object sender, FileCopiedClass e)
        {
            int pos = Console.CursorTop;
            Console.WriteLine("Pos is {0}", pos);
            Console.WriteLine("Progress Copied: {0:0.00}%" , (float)e.progress/e.actualSize*100);
            Console.WriteLine("Pos is now {0}", Console.CursorTop);
            Console.SetCursorPosition(0, pos);
        }

        public static void Main()
        {
            Console.WriteLine("Testing CopyLearning");
            //CopyFile()
            Progress<FileCopiedClass> p = new Progress<FileCopiedClass>();
            p.ProgressChanged += p_ProgressChanged;
            Task ta = CreateRandomFile(@"D:\Programming\Testing\RandomFile.asd", 999999999, p);
            ta.Wait();
        }
    }

    class FileCopiedClass
    {
        public ulong progress;
        public ulong actualSize;
    }
}
