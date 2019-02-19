using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LU.BLL.Section.RedisCacheTime
{
    public class CacheTimeElement : ConfigurationElement
    {
        [ConfigurationProperty("source", IsRequired = true)]
        public string Source
        {
            get { return this["source"].ToString(); }
        }

        [ConfigurationProperty("time", IsRequired = true)]
        public int Time
        {
            get { return int.Parse(this["time"].ToString()); }
        }
    }
}
