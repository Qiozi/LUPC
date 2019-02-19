using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Xml;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace LUComputers.Watch
{
    public  class Supercom
    {
        public event Events.UrlEventHandler WatchE;

        protected void InvokeWatchE(Events.UrlEventArgs e)
        {
            Events.UrlEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        public void SetStatus(string url, string comment, string result )
        {
            LUComputers.Events.UrlEventArgs ua = new LUComputers.Events.UrlEventArgs(new Events.UrlEventModel()
            {
                comment = comment,
                url = url,
                ltd = Ltd.wholesaler_supercom,
                result = result 
            });
            InvokeWatchE(ua);
        }
        void SetStatus(string url, string comment)
        {
            SetStatus(url, comment, null);
        }
        public void SetStatus(string result)
        {
            SetStatus(null, null, result);
        }

        public Supercom() { }

       
        public string Rfilter(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            else
                return str.Replace("/", "[Qright]").Replace("\\", "[Qleft]").Replace("*", "[Qstar]").Replace("<", "[QXiao]").Replace(">", "[QDa]").Replace("?", "[QWen]");
        }

        public string Lfilter(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            else
                return str.Replace("[Qright]", "/").Replace("[Qleft]", "\\").Replace("[Qstar]", "*").Replace("[QXiao]", "<").Replace("[QDa]", ">").Replace("[QWen]", "?");
        }     
    }

    public class LoadRunEventArgs : System.EventArgs
    {
        private string _LoadRunSku;

        public string viewString
        {
            get { return _LoadRunSku; }
        }

        public LoadRunEventArgs(string LoadRunSku)
        {
            this._LoadRunSku = LoadRunSku;
        }
    }
}
