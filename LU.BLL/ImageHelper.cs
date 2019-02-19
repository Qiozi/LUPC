using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class ImageHelper
    {
        public static string Get(string imagefilename)
        {
            if (!string.IsNullOrEmpty(imagefilename))
            {
                return string.Concat(Config.ResHost, imagefilename);
            }
            return imagefilename;
        }
    }
}
