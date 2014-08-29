using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.qas.sambo.directoryupdate.Utils
{
    class ZipEncryption
    {
        private Random random;
        // number of characters so the last few digits aren't added
        private readonly String CharacterAlphabet = "abcdefghijklmnopqrstuvwxyz";
        private readonly String CharactersNonAlphabet = "!@#$%&^*";
        private readonly String CharacterList;
        
        public ZipEncryption()
        {
            random = new Random((int)DateTime.Now.Ticks);
            CharacterList = CharacterAlphabet + CharactersNonAlphabet;
        }

        /// <summary>
        /// Will generate a random password with length size. The characters chosen will be from a list
        /// combination of CharacterAlphabet and CharactersNonAlphabet. 
        /// </summary>
        /// <param name="size">The length of the password to be generated</param>
        /// <returns></returns>
        public string EncryptionPasswordGenerator(int size)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                // To ensure that the charactersNonAlphabet is not counted twice
                // Dont want !@#!%$ etc2 to be capitalized and returned 
                int CharacterToChoose = Convert.ToInt32(Math.Floor((CharacterAlphabet.Length * 2 + CharactersNonAlphabet.Length) * random.NextDouble()));
                builder.Append(ReturnCharacterList(CharacterToChoose));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Will return the character either with a lower case or upper case
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private char ReturnCharacterList(int CharacterToChoose)
        {
            if (CharacterToChoose >= CharactersNonAlphabet.Length + CharacterAlphabet.Length)
                return CharacterList.ElementAt(CharacterToChoose - (CharactersNonAlphabet.Length + CharacterAlphabet.Length));
            else
                return Char.ToUpper(CharacterList.ElementAt(CharacterToChoose));
        }

        /// <summary>
        /// Will zip up pathToZip with password password and output the .zip file 
        /// at path zipPath
        /// </summary>
        /// <param name="pathToZip">path to zip</param>
        /// <param name="password">Password</param>
        /// <param name="zipPath">Output Path (Has to be .zip format. e.g. Output.zip</param>
        public void ZipWithEncryption(string pathToZip, string password, string zipPath)
        {
            
            using (ZipFile zip = new ZipFile())
            {
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = password;
                Console.WriteLine("Adding Directory of path");
                zip.AddDirectory(pathToZip);
                Console.WriteLine("Saving Files");
                zip.Save(zipPath);
            }
        }

        public static void Main()
        {
            ZipEncryption a = new ZipEncryption();
        
            ///CharactertoChooseTest
            //
            for (int i = 0; i < a.CharacterAlphabet.Length * 2 + a.CharactersNonAlphabet.Length; i++)
            //for (int i = 0; i < 73; i++) 
            {
                Console.WriteLine("I is {0} and charc is {1}", i, a.ReturnCharacterList(i));
            }
            for (int i = 0; i <= 10000000; i++)
            {
                Console.WriteLine("Password is {0}", a.EncryptionPasswordGenerator(8));
            }
        }
    }
}

