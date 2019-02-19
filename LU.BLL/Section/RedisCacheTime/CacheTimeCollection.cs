using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LU.BLL.Section.RedisCacheTime
{
    public class CacheTimeCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheTimeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheTimeElement)element).Source;
        }

        public CacheTimeElement this[int index]
        {
            get
            {
                return this.BaseGet(index) as CacheTimeElement;
            }
        }

        new public CacheTimeElement this[string Name]
        {
            get
            {
                return (CacheTimeElement)BaseGet(Name);
            }
        }
        new public int Count
        {
            get { return base.Count; }
        }

    }
}
