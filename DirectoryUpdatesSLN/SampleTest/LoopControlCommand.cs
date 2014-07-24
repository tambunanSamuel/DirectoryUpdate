using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.directoryupdate.SampleTest
{
    class LoopControlCommand
    {
        public static void Main()
        {
            Console.WriteLine("Enter Command:");
            var command = Console.Read();

            while (!command.Equals('q'))
            {
                
                //Console.WriteLine("Command is q? {0}",command.Equals('q'));
                switch(command)
                {
                    case 'a':
                        Console.WriteLine("Case 1");
                        break;
                    case 'b':
                        Console.Write("Case b");
                        break;
                    default:
                        Console.WriteLine("hohoho");
                        break;
                }

                Console.WriteLine("Final\n\n");

                    command = Console.Read();
            }
        }

        public static void Test1()
        { Console.WriteLine("Test1"); }

        public static void Test2()
        { Console.WriteLine("Test2"); }

        public static void Test3()
        { Console.WriteLine("Test3"); }

    }
}
