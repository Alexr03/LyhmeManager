using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace LyhmeManagerLibary
{
    public class Keys
    {
        public static bool t;

        public static void CheckKey(string Key)
        {
            bool enabled = false;
            var http = (HttpWebRequest)WebRequest.Create("http://solarsentinels.co.uk/keys.txt");
            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            
            if (content.Contains(Key)) { enabled = true; }
            else { enabled = false; }
            t = enabled;
        }
    }
}
