using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.Events
{
    public class EventBase
    {
        public event Events.MyEventHandler WatchE;

        protected void InvokeWatchE(Events.MyEventArgs e)
        {
            Events.MyEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        public void SetStatus(string comment)
        {
            Events.MyEventArgs ua = new Events.MyEventArgs(new Events.MyEventModel()
            {
                comment = comment
            });
            InvokeWatchE(ua);
        }

    }
}
