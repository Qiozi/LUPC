using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public static class Config
    {
        public static string Host = System.Configuration.ConfigurationManager.AppSettings["fullHost"];// "https://www.lucomputers.com/";

        public static string ResHost = "https://www.lucomputers.com/"; //Host;

        public const string QiNiuImgUrl = "https://o9ozc36tl.qnssl.com/";

        public static string IsLocalHost = System.Configuration.ConfigurationManager.AppSettings["hostName"];//"lu.com";

        public const int SysCaseCommentId = 9;

        public const string eBayUrl = "http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=";

        /// <summary>
        /// 412: barebone
        /// 413: gaming pc (default)
        /// 414: home pc(low 1000$)
        /// </summary>
        public static int[] SysCateIds = { 412, 413, 414 };


        public static int relation_motherboard_video_group_id = 229;
        public static int relation_motherboard_audio_group_id = 230;
        public static int relation_motherboard_network_group_id = 233;


        public static bool IsFileCache = System.Configuration.ConfigurationManager.AppSettings["IsFileCache"] == "1";
        public static string FileCachePath = System.Configuration.ConfigurationManager.AppSettings["FileCachePath"];

        public static string WebSitePhysicalPath { get; set; }

        public static string RedisExchangeHosts
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["RedisExchangeHost"].ConnectionString;
            }
        }
    }
}
