using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LU.BLL.Section.RedisCacheTime
{
    public class ConfigSection : ConfigurationSection
    {
        private static ConfigSection _Instance = null;

        public static ConfigSection Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = ConfigurationManager.GetSection("RedisCacheTime") as ConfigSection;
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 全部用相同的缓存有效时间;
        /// 如果值为0，使用子节点的配置时间
        /// </summary>
        [ConfigurationProperty("allSameTime", IsRequired = true)]
        public int allSameTime
        {
            get
            {
                return int.Parse(this["allSameTime"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("CacheTime", IsDefaultCollection = true)]
        public CacheTimeCollection CacheTimes
        {
            get { return this["CacheTime"] as CacheTimeCollection; }
        }
    }
}
