using System;
using System.Collections.Generic;
using System.Text;

namespace LUComputers.Events
{
    /// <summary>
    /// 事件委托
    /// </summary>
    /// <param name="sender">事件对象</param>
    /// <param name="e">事件参数</param>
    public delegate void UrlEventHandler(object sender, UrlEventArgs e);
    /// <summary>
    /// 事件
    /// </summary>
    public class UrlEventArgs : EventArgs
    {
        public UrlEventModel _urlEventModel { get; set; }

        public UrlEventArgs(UrlEventModel uem)
        {
            _urlEventModel = uem;
        }
    }
    /// <summary>
    /// 事件参数实体类
    /// </summary>
    public class UrlEventModel
    {
        public string url { set; get; }
        public string comment { set; get; }
        public Ltd ltd { set; get; }
        public string result { set; get; }
    }
}
