using System;
using System.Collections.Generic;
using System.Text;

namespace KKWStore.Helper
{
    public class TaobaoConfig
    {
        public static bool IsTest = false;

        public static string AppKey = System.Configuration.ConfigurationManager.AppSettings["appKey"].ToString();// "12157804";//"12156166";
        public static string AppSecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"].ToString();//"686f8b2f3f77e2d19f31e4dd4324257d";//"b3e7488a5fdba0e4d45abf067df185dc";
       /// <summary>
       /// api 访问
       /// </summary>
        public static string UrlAPI
        {
            get
            {
                if (IsTest)
                {
                    return "http://gw.api.tbsandbox.com/router/rest";
                }
                else
                    return "http://gw.api.taobao.com/router/rest";
            }
        }
        public static string SessionKey = "";//"282697d624365a5511f93ecc10f983dd45688";
        /// <summary>
        /// 取得sessionKey 
        /// </summary>
        public static string GetAuthorize
        {
            get
            {
                if (IsTest)
                    return "http://container.api.tbsandbox.com/container?authcode=" + AuthorizeCode;
                else
                    return "http://container.open.taobao.com/container?authcode={" + AuthorizeCode + "}";
            }
        }
        /// <summary>
        /// 登入
        /// </summary>
        public static string LoginUrl
        {
            get
            {
                if (IsTest)
                {
                    return "http://open.taobao.com/isv/authorize.php?appkey=" + AppKey;
                }
                else
                {
                    return "http://open.taobao.com/isv/authorize.php?appkey=" + AppKey + "";
                }
            }
        }

        public static string AuthorizeCode = "";
        /// <summary>
        /// 数据备份路径
        /// </summary>
        public static string BackupPath = (System.Configuration.ConfigurationManager.AppSettings["backupPath"].ToString() == "") ? System.Windows.Forms.Application.StartupPath : System.Configuration.ConfigurationManager.AppSettings["backupPath"].ToString();


        public static string StoreName(string key)
        {
            if (key == "")
                return "现代经典厨具";
            if (key == "")
                return "旺旺我的家";
            if (key == "")
                return "";

            return "";
        }
        /// <summary>
        /// 获取三个app key
        /// </summary>
        /// <returns></returns>
        public static List<TaobaoKey> GetTaobaoKeys() {
            List<TaobaoKey> tks = new List<TaobaoKey>();

            string[] appkeys = Helper.TaobaoConfig.AppKey.Split(new char[] { '|' });
            string[] appSecrets = Helper.TaobaoConfig.AppSecret.Split(new char[] { '|' });
            string[] names =  System.Configuration.ConfigurationManager.AppSettings["AppName"].ToString().Split(new char[]{'|'});
            int n = 0;
            foreach (var s in appkeys)
            {
                if (s.Trim() == "") continue;
                tks.Add(new TaobaoKey()
                {
                    AppKey = s,
                    AppSecret = appSecrets[n],
                    name = names[n]
                });
                n += 1;
            }

            return tks;
        } 
    }

    public class TaobaoKey
    {
        public string AppKey { set; get; }
        public string AppSecret { set; get; }
        public string name { set; get; }
    }
}
