using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        public static string ReadTextFromUrl(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
