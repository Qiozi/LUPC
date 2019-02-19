using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace LUComputers.Helper
{
    public class ProcessHelper
    {
        public ProcessHelper() { }

        #region process constrol
        public static int SubControlMaxSum = 100;
        public static int SubControlCurrentValue = 1;

        public static int ParentControlMaxSum = 7;
        public static int ParentControlCurrentValue = 1;

        #endregion

        public string  LoadFromLocal(string filename, string sql)
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";// "C:\\Program Files\\MySQL\\MySQL Server 5.0\\bin\\mysql.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;

                string cmd = string.Format(@"mysql -hlocalhost -uroot -p1234qwer ltd_info -B -e ""{0};"" > {1}",
                sql, filename );
                p.Start();
                p.StandardInput.WriteLine(cmd);

                p.StandardInput.WriteLine("exit");
                string output = p.StandardOutput.ReadToEnd();
                return output;
            }
        }
    }
}
