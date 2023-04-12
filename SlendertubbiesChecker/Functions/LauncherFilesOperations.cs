using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SlendertubbiesChecker.Functions
{
    internal class LauncherFilesOperations
    {
       
        public static void PlayGame()
        {
            Directory.SetCurrentDirectory(@"Slendertubbies");
            Process.Start("Slendertubbies.exe");
            
        }
        

        public static bool DoesFileExists(string sciezkaRelatywna)
        {
            return File.Exists(sciezkaRelatywna);
        }

        internal static string CheckLocalVersion(string FilePatch)
        {
            string z;
            try
            {
                using (TextReader reader = File.OpenText(FilePatch))
                {
                    z = reader.ReadLine();

                }

            }
            catch (Exception)
            {
                z = ("Invalid");
            }
            return z;
        }
    }
}
