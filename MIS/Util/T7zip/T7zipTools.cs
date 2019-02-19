using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Util.T7zip
{
    public class T7zipTools
    { 
        public static bool isUnZipFileRun = false;

        public static string ZipExeFullname = "7za.exe";

        public static bool ZipDir(string dir, string toFile)
        {
            try
            {
                if (!File.Exists(ZipExeFullname))
                    throw new FileNotFoundException("7za.exe 文件不存在");
                ProcessStartInfo p = new ProcessStartInfo();
                p.FileName = ZipExeFullname;

                p.Arguments = " a \"" + toFile + "\" \"" + dir + "\" ";
                p.WindowStyle = ProcessWindowStyle.Hidden;

                Process x = Process.Start(p);
                x.WaitForExit();

                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public static bool ZipFile(string filefullname, string tofile)
        {
            try
            {
                if (!File.Exists(ZipExeFullname))
                    throw new FileNotFoundException(ZipExeFullname + "  7za.exe文件不存在");
                ProcessStartInfo p = new ProcessStartInfo();
                p.FileName = ZipExeFullname;

                p.Arguments = " a \"" + tofile + "\" \"" + filefullname + "\" ";
                p.WindowStyle = ProcessWindowStyle.Hidden;
               
                Process x = Process.Start(p);
                x.WaitForExit();

                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public static bool ZipFileRecurse(string dir, string toFile)
        {
            try
            {
                if (!Directory.Exists(dir))
                    return false;
                if (!File.Exists(ZipExeFullname))
                    throw new FileNotFoundException(ZipExeFullname+ "  7za.exe 文件不存在");
                ProcessStartInfo p = new ProcessStartInfo();
                p.FileName = ZipExeFullname;

                p.Arguments = " a " + toFile + " " + dir + " -r";
                p.WindowStyle = ProcessWindowStyle.Hidden;

                Process x = Process.Start(p);
                x.WaitForExit();
               
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public static bool UnZipFile(string zipFullname, string toDir, string fileExt, bool deleteZip)
        {
            try
            {
                if (File.Exists(zipFullname))
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = ZipExeFullname;
                    //proc.StartInfo.Arguments = " e " + zipFullname + 
                    //    @" -aoz ""-o" + toDir + @"""";

                    proc.StartInfo.Arguments = " x \"" + zipFullname + "\" -aoa \"-o" + toDir + "\" " + fileExt;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                 
                    proc.Start();

                    proc.WaitForExit();

                    while (!proc.HasExited)
                    {
                        Thread.Sleep(100);
                    }
              
                    if (deleteZip)
                        File.Delete(zipFullname);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Util.Logs.WriteErrorLog(ex);
                // throw ex;
            }
            return false;
        }

        public static bool UnZipFile(string zipFullname, string toDir)
        {
            return UnZipFile(zipFullname, toDir, "", false);
        }

        public static bool UnZipFile(string zipFullname, string toDir, bool deleteZip)
        {
            return UnZipFile(zipFullname, toDir, "", deleteZip);
        }
    }
    
}
