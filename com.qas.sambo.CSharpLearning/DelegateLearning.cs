using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.CSharpLearning
{
    class DelegateLearning
    {
        // Delegates are mostly used for encapsulation

        public DelegateLearning()
        {

        }

        public delegate void HelloDelegate(string s);

        public void HelloWorld(string s)
        {
            Console.WriteLine("Hello World {0}", s);
        }

        public void HelloSunshine(string s)
        {
            Console.WriteLine("Hello Sunshine {0}", s);
        }

        public void run()
        {
            HelloDelegate hello = HelloWorld;
            // Will print out Hello World Sam
            hello("Sam");
            
            // will print Hello World John
            hello = HelloSunshine;
            hello("John");

        }
    }
}
