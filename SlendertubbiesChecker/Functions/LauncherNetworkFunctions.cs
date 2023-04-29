using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SlendertubbiesChecker.Functions
{
    internal class LauncherNetworkFunctions
    {
        public bool CanConnectToServer()

        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://leafq.online/"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        //Reads string from Url
        public static string ReadTextFromUrl(string url)
        {
            using (var client = new WebClient())
            {
                try
                {
                    return client.DownloadString(url);
                }
                catch (Exception)
                {
                    MessageBox.Show("This program was unable to connect to Slendertubbies servers!", "Critical error!");
                    return null;
                }

            }
        }
    }
}
