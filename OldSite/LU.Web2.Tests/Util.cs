using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Web2.Tests
{
    public class Util
    {
        public static LU.Data.nicklu2Entities DBContext
        {
            get { return new LU.Data.nicklu2Entities(); }
        }
    }
}
