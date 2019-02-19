using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Util
{
    public class ProcessHelper
    {
        public static void Open(string url)
        {
            if (File.Exists(url))
            {
                Process p = new Process();
                p.StartInfo.FileName = url;
                p.Start();
            }
            else
                throw new ArgumentException("File is not exist.");
        }
    }
}
