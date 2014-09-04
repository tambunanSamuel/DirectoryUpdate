using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.CSharpLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            DelegateRunningMethod();
        }

        public static void DelegateRunningMethod()
        {
            DelegateLearning dl = new DelegateLearning();
            dl.run();
        }
    }
}
