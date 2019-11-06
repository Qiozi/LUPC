using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunStore.Toolkits
{
    public class Util
    {
        public string GetCurrDateTime
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffff"); }
        }
    }
}
