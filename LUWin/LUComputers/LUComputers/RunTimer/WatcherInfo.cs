using System;
using System.Collections.Generic;
using System.Text;

namespace LUComputers.RunTimer
{
    public class WatcherInfos
    {
        
        
        //public static bool RunASI = false;
        //public static long AsiFileLength = 0;
        //public static bool RunSupercom = false;
        //public static long SupercomFileLength = 0;
        //public static bool RunDanDh = false;
        //public static long DandhLength = 0;
        //public static bool RunSynnex = false;
        //public static long SynnexFileLength = 0;
        //public static bool RunEprom = false;
        //public static long EpromFileLength = 0;
        //public static bool RunD2a = false;
        //public static long D2AFileLength = 0;
        //public static bool RunMMAX = false;
        //public static long MMAXFileLength = 0;

        public static WatcherInfo ASI = new WatcherInfo();
        public static WatcherInfo Supercom = new WatcherInfo();
        public static WatcherInfo SupercomALL = new WatcherInfo();
        public static WatcherInfo DanDh = new WatcherInfo();
        public static WatcherInfo Synnex = new WatcherInfo();
        public static WatcherInfo Eprom = new WatcherInfo();
        public static WatcherInfo D2A = new WatcherInfo();
        public static WatcherInfo MMAX = new WatcherInfo();
        
        
    }

    public class WatcherInfo
    {
        public bool begin { set; get; }
        bool _running = false;
        public bool running
        {
            set { _running = value; }
            get { return _running; }
        }
        public long length { set; get; }
        public bool end { set; get; }
    }
}
