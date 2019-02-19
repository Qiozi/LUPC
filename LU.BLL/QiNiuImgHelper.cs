using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class QiNiuImgHelper
    {
        public static string GetUrl(string imgFilename)
        {
            if (string.IsNullOrEmpty(imgFilename))
            {
                return string.Empty;
            }
            if (imgFilename.ToLower().IndexOf("uploads") > -1 ||
                imgFilename.ToLower().IndexOf("QRCodes".ToLower()) > -1)
            {
                return string.Concat(Config.QiNiuImgUrl, imgFilename.Split(new char[] { '/' })[3]);
            }

            return string.Concat(Config.QiNiuImgUrl, imgFilename);
        }

        public static string RemoveUrl(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return filename;
            else
                return filename.Replace(Config.QiNiuImgUrl, "");
        }

        public static string Get(int sku, int w = 300, int h = 300, int i = 0)
        {
            if (w == 50)
            {
                return string.Format(@"{1}pro_img/components/{0}_t.jpg", sku, Config.ResHost, i == 0 ? 1 : i);
            }
            if (w == 300)
            {
                return string.Format(@"{1}pro_img/components/{0}_list_1.jpg", sku, Config.ResHost);
            }
            if (w > 300)
            {
                return string.Format(@"{1}pro_img/components/{0}_g_{2}.jpg", sku, Config.ResHost, i == 0 ? 1 : i);
            }

            return string.Format(@"{1}pro_img/components/{0}_list_1.jpg", sku, Config.ResHost);
        }


        //public static bool ConvertImgUrlByQiNiu(List<M.Model.MM.TopicItem> list)
        //{
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        ConvertImgUrlByQiNiu(list[i]);
        //    }
        //    return true;
        //}

        //public static bool ConvertImgUrlByQiNiu(M.Model.MM.TopicItem item)
        //{
        //    // for (int i = 0; i < list.Count; i++)
        //    {
        //        if (item.Gallery != null)
        //        {
        //            for (int j = 0; j < item.Gallery.Count; j++)
        //            {
        //                item.Gallery[j].ImgUrl = GetUrl(item.Gallery[j].ImgUrl);
        //            }
        //        }

        //        item.Author.FaceLogo = GetUrl(item.Author.FaceLogo);
        //        if (item.TopicApplyItems != null)
        //        {
        //            for (int i = 0; i < item.TopicApplyItems.Count; i++)
        //            {
        //                if (!string.IsNullOrEmpty(item.TopicApplyItems[i].Avatar))
        //                {
        //                    item.TopicApplyItems[i].Avatar = GetUrl(item.TopicApplyItems[i].Avatar);
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}
    }
}
