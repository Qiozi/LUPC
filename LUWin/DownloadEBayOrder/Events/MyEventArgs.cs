using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.Events
{
    /// <summary>
    /// 事件
    /// </summary>
    public class MyEventArgs : EventArgs
    {
        public MyEventModel _urlEventModel { get; set; }

        public MyEventArgs(MyEventModel uem)
        {
            _urlEventModel = uem;
        }
    }
    /// <summary>
    /// 事件参数实体类
    /// </summary>
    public class MyEventModel
    {
        public string comment { set; get; }
    }
}
