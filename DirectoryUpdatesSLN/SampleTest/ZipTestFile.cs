﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;


namespace com.qas.sambo.directoryupdate.SampleTest
{
    class ZipTestFile
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        private static readonly String CharacterList = "abcdefghijklmnopqrstuvwxyz!@#$%&";
        private string password;
        public static void Main()
        {

            // Testing of ZipFile
            Console.WriteLine("Testing the EncryptionPasswordGenerator()");
            for (int i = 0; i < 10; i++ )
                Console.WriteLine("Password is {0}", EncryptionPasswordGenerator(8));

            Console.WriteLine("Insert Directory:\n");
            string cc = Console.ReadLine();
            Console.WriteLine("Ccc is {0}", cc);
            Console.WriteLine("Current Directory is at {0}", System.IO.Directory.GetCurrentDirectory());
            ZipDirectory(cc,"123456");
        }

        public static string EncryptionPasswordGenerator(int size)
        {
            StringBuilder builder = new StringBuilder();
            // There should be 32 characters here
            //32 + 26 = 58 characters to choose from
            char ch, CharC;
            /*for (int i = 0 ; i< size;i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                int CharacterToChoose = Convert.ToInt32(Math.Floor(64 * random.NextDouble()));
                Console.WriteLine("Random is {0}", CharacterToChoose);
                char CharC;

                if (CharacterToChoose > 32)
                    CharC = CharacterList.ElementAt(CharacterToChoose);
                else
                    CharC = Char.ToUpper(CharacterList.ElementAt(CharacterToChoose));
                
                Console.WriteLine("Value Charcter is{0}", CharC);
                builder.Append(ch);
            }*/
            /*for (int i = 0; i < 58; i++)
            {
                if (i >= 32)
                    CharC = CharacterList.ElementAt(i-32);
                else
                    CharC = Char.ToUpper(CharacterList.ElementAt(i));

                Console.WriteLine("I is {0} and Character is {1}", i, CharC);
            }*/

            for (int i = 0; i < size; i++ )
            {
                int CharacterToChoose = Convert.ToInt32(Math.Floor(64 * random.NextDouble()));
                if (CharacterToChoose >= 32)
                    CharC = CharacterList.ElementAt(CharacterToChoose - 32);
                else
                    CharC = Char.ToUpper(CharacterList.ElementAt(CharacterToChoose));
                builder.Append(CharC);
            }
                return builder.ToString();
        }

        public static void ZipDirectory(string path, string password)
        {
            String savedFile = @"test.zip";
            using (ZipFile zip = new ZipFile())
            {
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = new ZipTestFile().password;
                Console.WriteLine("Adding Directory of path");
                zip.AddDirectory(path);
                Console.WriteLine("Saving Files");
                zip.Save(savedFile);
            }
        }
    }

   


}
