using System;
using System.Collections.Generic;
using System.Text;

namespace LUComputers.Watch
{
    public class eBay
    {
        public event Events.UrlEventHandler WatchE;

        protected void InvokeWatchE(Events.UrlEventArgs e)
        {
            Events.UrlEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        public void SetStatus(string url, string comment, string result)
        {
            LUComputers.Events.UrlEventArgs ua = new LUComputers.Events.UrlEventArgs(new Events.UrlEventModel()
            {
                comment = comment,
                url = url,
                ltd = Ltd.eBay,
                result = result
            });
            InvokeWatchE(ua);
        }

        public eBay() { }

        //public bool WatchRun()
        //{

        //}
    }
}
