using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class Logs
    {
        public static void WriteLog(string LogString, string url = "", string urlReferrer = "", string prevStr = "")
        {
            Mutex MessageLog = null;
            bool isRelease = false;
            try
            {
                if (!System.IO.Directory.Exists(Config.WebSitePhysicalPath + "..\\Web.Log\\"))
                {
                    System.IO.Directory.CreateDirectory(Config.WebSitePhysicalPath + "..\\Web.Log\\");
                }

                MessageLog = new Mutex(false, DateTime.Now.ToString("yyyy-MM-dd_"));
                isRelease = MessageLog.WaitOne(5000, true);

                if (isRelease)
                {
                    var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string LogFileFullName = Config.WebSitePhysicalPath + "..\\Web.Log\\" + DateTime.Now.ToString("yyyy-MM-dd___") + prevStr + "___Message.Log";

                    System.IO.File.AppendAllText(LogFileFullName, date + " : " + LogString + "\r\n", Encoding.UTF8);

                    if (!string.IsNullOrEmpty(url))
                        System.IO.File.AppendAllText(LogFileFullName, date + " : [URL]" + url + "\r\n", Encoding.UTF8);

                    if (!string.IsNullOrEmpty(urlReferrer))
                        System.IO.File.AppendAllText(LogFileFullName, date + " : [URLReferrer Or Headers]" + urlReferrer + "\r\n", Encoding.UTF8);
                    System.IO.File.AppendAllText(LogFileFullName, "---------------------------------------------------------\r\n", Encoding.UTF8);
                }
            }
            catch (Exception)
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

        public static void WriteErrorLog(System.Exception exp, string url = "", string urlReferrer = "", string prevStr = "")
        {
            Mutex MessageLog = null;
            bool isRelease = false;
            try
            {
                if (!System.IO.Directory.Exists(Config.WebSitePhysicalPath + "..\\Web.Log\\"))
                {
                    System.IO.Directory.CreateDirectory(Config.WebSitePhysicalPath + "..\\Web.Log\\");
                }
                MessageLog = new Mutex(false, DateTime.Now.ToString("yyyy-MM-dd_"));
                isRelease = MessageLog.WaitOne(5000, true);
                if (isRelease)
                {
                    string ErrorFileFullName = Config.WebSitePhysicalPath + "..\\Web.Log\\" + DateTime.Now.ToString("yyyy-MM-dd___") + prevStr + "___Error.Log";
                    string ErrorString = "============================================\r\n";
                    ErrorString += "DateTime    = " + DateTime.Now.ToString() + "\r\n";
                    ErrorString += "Message     = " + exp.Message + "\r\n";
                    ErrorString += "Source      = " + exp.Source + "\r\n";
                    ErrorString += "StackTrace  = " + exp.StackTrace + "\r\n";
                    ErrorString += "TargetSite  = " + exp.TargetSite + "\r\n";
                    if (!string.IsNullOrEmpty(url))
                        ErrorString += "Request Url = " + url + "\r\n";
                    if (!string.IsNullOrEmpty(urlReferrer))
                        ErrorString += "Request UrlReferrer = " + urlReferrer + "\r\n";
                    ErrorString += "============================================\r\n";

                    System.IO.File.AppendAllText(ErrorFileFullName, ErrorString + "\r\n", Encoding.UTF8);
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
