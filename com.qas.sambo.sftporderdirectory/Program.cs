using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.sftporderdirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = Console.ReadLine();
            char? b = command[0];
            while (b !='q')
            {
                Console.WriteLine("is b != q {0} and {1}", b != 'q',b);

                //Console.WriteLine("Command is q? {0}",command.Equals('q'));
                switch (b)
                {
                    case 'a':
                        Console.WriteLine("Case 1");
                        break;
                    case 'b':
                        Console.Write("Case b");
                        break;
                    default:
                        Console.WriteLine("hohoho");
                        Console.WriteLine("B is {0}", b);
                        break;
                }

                Console.WriteLine("Final\n\n");
                // trimmed to remove leading spaces
                command = Console.ReadLine().Trim();
                String[] first = command.Split(new char[] {' '}, 2);
                foreach (string s in first)
                {
                    Console.WriteLine("S is {0}", s);
                }
                b = (command.Equals(" "))? ' ':command[0];
            }
        }

        public String[] SplitInput(String command)
        {
            String[] Inputs = new String[2];
            command = command.Trim();


            if (!command.Equals("") && command.Contains(" "))
            {
                Inputs = command.Split(new char[] { ' ' }, 2);
            } 
            else
            {
                Inputs[0] = "";
                Inputs[1] = "";
            }

            return Inputs;
        }
    }
}
