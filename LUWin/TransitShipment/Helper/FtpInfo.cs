using System;
using System.Collections.Generic;
using System.Text;

namespace TransitShipment.Helper
{
    public class FtpInfo
    {
        public FtpInfo() {           
        
        }

        public FInfo Synnex { set; get; }
        public FInfo Dandh { set; get; }
        public FInfo ASI { get; set; }

        /// <summary>
        /// www.luwebmaster.com/
        /// </summary>
        public FInfo WebManage { get; set; }
    }

    public class FInfo
    {
        public FInfo() { }
        public string Ip { set; get; }
        public string Pwd { set; get; }
        public string Uid { set; get; }
        public int Port { set; get; }

        public string SavePath { set; get; }
        public string SaveFilename { get; set; }
    }
}
