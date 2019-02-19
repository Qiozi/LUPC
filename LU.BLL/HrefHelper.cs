using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class HrefHelper
    {
        public static string GetSysCate(int cid)
        {
            return string.Concat("/list_sys.aspx?cid=", cid);
        }

        public static string GetPartCate(int cid)
        {
            return string.Concat("/list_part.aspx?cid=", cid);
        }

        public static string GetCateList(int cid, string cateName)
        {
            if (!string.IsNullOrEmpty(cateName))
            {
                return string.Concat("/Computer/", cateName, ".html");
            }
            return string.Concat("/list_cate.aspx?cid=", cid);
        }
    }
}
