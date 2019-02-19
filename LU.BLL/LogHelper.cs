using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class LogHelper
    {
        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        public static void WriteLog(Type t, string errMsg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(errMsg);
        }
    }
}
