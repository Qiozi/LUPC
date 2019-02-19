using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutoExecute
{
    public static class Logs
    {
        public static void WriteLog(string LogString)
        {
            Mutex MessageLog = null;
            bool isRelease = false;
            try
            {
                if (!System.IO.Directory.Exists(".\\Logs\\"))
                {
                    System.IO.Directory.CreateDirectory(".\\Logs\\");
                }

                MessageLog = new Mutex(false, DateTime.Now.ToString("yyyyMMdd"));
                isRelease = MessageLog.WaitOne(5000, true);

                if (isRelease)
                {
                    string LogFileFullName = ".\\Logs\\" + DateTime.Now.ToString("yyyyMMdd") + "Message.Log";

                    System.IO.File.AppendAllText(LogFileFullName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + LogString + "\r\n", Encoding.Unicode);
                }
            }
            catch (System.Exception)
            {

            }
            finally
            {
                if (isRelease)
                {
                    MessageLog.ReleaseMutex();
                }
            }
        }

        public static void WriteErrorLog(System.Exception exp)
        {
            Mutex MessageLog = null;
            bool isRelease = false;
            try
            {
                if (!System.IO.Directory.Exists(".\\Logs\\"))
                {
                    System.IO.Directory.CreateDirectory(".\\Logs\\");
                }
                MessageLog = new Mutex(false, DateTime.Now.ToString("yyyyMMdd"));
                isRelease = MessageLog.WaitOne(5000, true);
                if (isRelease)
                {
                    string ErrorFileFullName = ".\\Logs\\" + DateTime.Now.ToString("yyyyMMdd") + "Error.Log";
                    string ErrorString = "============================================\r\n";
                    ErrorString += "DateTime     = " + DateTime.Now.ToString() + "\r\n";
                    ErrorString += "Message       = " + exp.Message + "\r\n";
                    ErrorString += "Source          = " + exp.Source + "\r\n";
                    ErrorString += "StackTrace    =" + exp.StackTrace + "\r\n";
                    ErrorString += "============================================\r\n";

                    System.IO.File.AppendAllText(ErrorFileFullName, ErrorString + "\r\n", Encoding.Unicode);
                }
            }
            catch (System.Exception)
            { }
            finally
            {
                if (isRelease)
                {
                    MessageLog.ReleaseMutex();
                }
            }
        }
    }
}
