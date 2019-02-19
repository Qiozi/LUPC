using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace LU.BLL
{
    public class HttpRuntimeHelper
    {
        /// <summary>
        /// 改用文件缓存， 2017-05-02
        /// </summary>
        public static bool IsFileCache = LU.BLL.Config.IsFileCache;
        public static string FileCachePath = LU.BLL.Config.FileCachePath;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheDependency"></param>
        /// <param name="datetime"></param>
        /// <param name="timeSpan"></param>
        /// <param name="cacheItemPriority"></param>
        /// <param name="cacheItemRemovedCallBack"></param>
        /// <returns></returns>
        public static bool Set(string key
            , object value
            , CacheDependency cacheDependency
            , DateTime datetime
            , TimeSpan timeSpan
            , CacheItemPriority cacheItemPriority
            , CacheItemRemovedCallback cacheItemRemovedCallBack)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return false;
            }
            else
            {
                HttpRuntime.Cache.Insert(key, value, cacheDependency, datetime, timeSpan
                    , cacheItemPriority, cacheItemRemovedCallBack);
                return true;
            }
        }

        public static object GetValue(string key)
        {
            return string.IsNullOrEmpty(key) ? null : HttpRuntime.Cache.Get(key);
        }
    }

    public enum CacheKey
    {
        ProdCategory,
        Prod,
        ProdPrice,
        Home,
        eBayCode,
        SysComment,
        /// <summary>
        /// 一個系統基本信息，用於系統列表
        /// </summary>
        SysBase
    }

    public class CacheHelper<T>
    {
        public static T GetValue(CacheKey key)
        {
            if (HttpRuntimeHelper.IsFileCache)
            {
                var setting = CacheProvider.GetCacheSettings().Single(c => c.CacheKey.Equals(key));

                var file = HttpRuntimeHelper.FileCachePath + "\\" + key.ToString() + ".json";
                if (System.IO.File.Exists(file))
                {
                    var fileInfo = new System.IO.FileInfo(file);
                    if ((DateTime.Now - fileInfo.LastWriteTime) < TimeSpan.FromSeconds(setting.ExpireTimeSeconds))
                    {
                        var content = System.IO.File.ReadAllText(file);
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
                    }
                }
                return default(T);
            }
            else
            {
                //var redis = new LU.BLL.Redis.RedisHelper(1);

                var value = HttpRuntimeHelper.GetValue(((int)key).ToString());

                return value == null ? default(T) : (T)value;
                //try
                //{
                //    return redis.StringGet<T>(key.ToString());
                //}
                //catch { }
                //return default(T);
            }
        }

        public static bool Set(CacheKey key, object value)
        {
            var setting = CacheProvider.GetCacheSettings().Single(c => c.CacheKey.Equals(key));

            if (HttpRuntimeHelper.IsFileCache)
            {
                var isWrite = false;
                var content = Newtonsoft.Json.JsonConvert.SerializeObject(value);
              
                var file = HttpRuntimeHelper.FileCachePath + "\\" + key.ToString() + ".json";
                if (System.IO.File.Exists(file))
                {
                    var fileInfo = new System.IO.FileInfo(file);
                    if ((DateTime.Now - fileInfo.LastWriteTime) > TimeSpan.FromSeconds(setting.ExpireTimeSeconds))
                    {
                        isWrite = true;
                    }
                }
                else
                {
                    isWrite = true;
                }

                if (isWrite)
                {
                    System.IO.File.WriteAllText(file, content, Encoding.UTF8);
                }
                return true;
            }
            else
            {
                var callback = new CacheItemRemovedCallback(CacheProvider.CallBack); // 缓存项失效，移除回调。                

                return HttpRuntimeHelper.Set(((int)key).ToString()
                      , value
                      , null
                      , System.Web.Caching.Cache.NoAbsoluteExpiration
                      , TimeSpan.FromSeconds(setting.ExpireTimeSeconds)
                      , setting.CachePriority
                      , callback);

                //var redis = new Redis.RedisHelper(1);
                //redis.StringSet(key.ToString(), value, new TimeSpan(0, 10, 0));
                //return true;
            }
        }
    }

    public class CacheSettingItem
    {
        public CacheKey CacheKey { get; set; }

        public int ExpireTimeSeconds { get; set; }

        public CacheItemPriority CachePriority { get; set; }
    }

    public class CacheProvider
    {
        public static List<CacheSettingItem> GetCacheSettings()
        {
            return new List<CacheSettingItem>()
               {
                     new CacheSettingItem { CacheKey = CacheKey.ProdCategory, CachePriority = CacheItemPriority.High, ExpireTimeSeconds= 600},
                     new CacheSettingItem { CacheKey = CacheKey.Home, CachePriority = CacheItemPriority.High, ExpireTimeSeconds = 360},
                     new CacheSettingItem { CacheKey = CacheKey.ProdPrice, CachePriority = CacheItemPriority.High , ExpireTimeSeconds = 360},
                     new CacheSettingItem { CacheKey = CacheKey.Prod, CachePriority = CacheItemPriority.High, ExpireTimeSeconds = 360},
                     new CacheSettingItem { CacheKey = CacheKey.eBayCode, CachePriority = CacheItemPriority.High , ExpireTimeSeconds = 600},
                     new CacheSettingItem { CacheKey = CacheKey.SysComment, CachePriority = CacheItemPriority.High , ExpireTimeSeconds = 600},
                     new CacheSettingItem { CacheKey = CacheKey.SysBase, CachePriority = CacheItemPriority.High , ExpireTimeSeconds = 600}
               };
        }

        public static void CallBack(string key, object value, CacheItemRemovedReason reason)
        {
            // TODO
        }

        public static List<Model.Cate> GetAllCates(Data.nicklu2Entities context)
        {
            var cates = CacheHelper<List<Model.Cate>>.GetValue(CacheKey.ProdCategory);
            if (cates == null || cates.Count == 0)
            {
                // throw new Exception("HD");
                cates = CateProvider.GetCates(context);
                CacheHelper<List<Model.Cate>>.Set(CacheKey.ProdCategory, cates);
            }
            return cates;
        }

        private static List<int> GetAllCateIds(List<Model.Cate> cates)
        {
            var cateIds = new List<int>();
            foreach (var cate in cates)
            {
                var query = cate.SubCates.Select(p => p.Id).ToList();
                cateIds.AddRange(query);
            }
            return cateIds;
        }

        public static List<Model.Product> GetAllProdBaseInfos(Data.nicklu2Entities context, int cateId)
        {
            var prods = CacheHelper<List<Model.Product>>.GetValue(CacheKey.Prod);
            if (prods == null || prods.Count == 0)
            {
                var cates = GetAllCates(context);
                var cateIds = GetAllCateIds(cates);
                prods = ProductProvider.GetAllProducts(context, 0, Model.Enums.CountryType.CAD, cateIds.ToArray());// ProductProvider.GetAllPartInfoForCache(context, cateIds, Model.Enums.CountryType.CAD);
                CacheHelper<List<Model.Product>>.Set(CacheKey.Prod, prods);
                // 暂时关闭 产品缓存
            }

            return prods;
        }

        public static List<Model.SysComment> GetSysCommentList(Data.nicklu2Entities context)
        {
            var comments = CacheHelper<List<Model.SysComment>>.GetValue(CacheKey.SysComment);
            if (comments == null || comments.Count == 0)
            {
                comments = ProductProvider.GetSysComments(context);
                CacheHelper<List<Model.SysComment>>.Set(CacheKey.SysComment, comments);
            }
            return comments;
        }

        public static List<Model.eBayMiniModel> GeteBayCodes(Data.nicklu2Entities context)
        {
            var ebayCodes = CacheHelper<List<Model.eBayMiniModel>>.GetValue(CacheKey.eBayCode);
            if (ebayCodes == null || ebayCodes.Count == 0)
            {
                ebayCodes = eBayProvider.GetAllParts(context);
                CacheHelper<List<Model.eBayMiniModel>>.Set(CacheKey.eBayCode, ebayCodes);
            }
            return ebayCodes;
        }

        public static List<Model.M.SysMiniModel> GetSysMiniBaseInfos(Data.nicklu2Entities context, System.Web.HttpContext httpContext)
        {
            var sysBaseInfos = CacheHelper<List<Model.M.SysMiniModel>>.GetValue(CacheKey.SysBase);
            if (sysBaseInfos == null || sysBaseInfos.Count == 0)
            {
                sysBaseInfos = ProductProvider.GetAllSystemsMiniInfo(context, httpContext);
                CacheHelper<List<Model.M.SysMiniModel>>.Set(CacheKey.SysBase, sysBaseInfos);
            }
            return sysBaseInfos;
        }
    }
}
