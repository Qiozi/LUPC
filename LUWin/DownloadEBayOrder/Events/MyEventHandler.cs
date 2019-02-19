using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.Events
{
    /// <summary>
    /// 事件委托
    /// </summary>
    /// <param name="sender">事件对象</param>
    /// <param name="e">事件参数</param>
    public delegate void MyEventHandler(object sender, Events.MyEventArgs e);
}
