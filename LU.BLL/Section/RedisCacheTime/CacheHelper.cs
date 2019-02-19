using LU.BLL.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LU.BLL.Section.RedisCacheTime
{
    public class CacheHelper<T>
    { /// <summary>
      /// 
      ///     @add by WuTianHui   2017-08-24
      ///     
      ///     Note: Set Info to Redis DB.
      ///     
      /// </summary>
      /// <param name="key"></param>
      /// <param name="value"></param>
        public static void SetCache(string key, int diff, object value, int dbNum, string customKey)
        {
            var time = 0; // 缓存有效期
            if (ConfigSection.Instance.allSameTime > 0)
            {
                time = ConfigSection.Instance.allSameTime;
            }
            else
            {
                time = ConfigSection.Instance.CacheTimes[key].Time;
            }

            var redis = new RedisHelper(dbNum, customKey);
            // 写入缓存
            redis.StringSet(GetFullKey(key, diff), value, new System.TimeSpan(0, 0, time));
        }

        /// <summary>
        /// 
        ///     @add by WuTianHui   2017-08-25
        ///     
        ///     Note: Set Info to Redis DB.
        ///     
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCache(string key, Guid diff, object value, int dbNum, string customKey)
        {
            var time = 0; // 缓存有效期
            if (ConfigSection.Instance.allSameTime > 0)
            {
                time = ConfigSection.Instance.allSameTime;
            }
            else
            {
                time = ConfigSection.Instance.CacheTimes[key].Time;
            }

            var redis = new RedisHelper(dbNum, customKey);
            // 写入缓存
            redis.StringSet(GetFullKey(key, diff), value, new System.TimeSpan(0, 0, time));
        }

        static string GetFullKey(string key, int diff)
        {
            return string.Concat(key, ":", diff);
        }

        static string GetFullKey(string key, Guid diff)
        {
            return string.Concat(key, ":", diff);
        }

        /// <summary>
        ///     
        ///     @add by WuTianHui   2017-08-24
        ///     
        ///     Note: get info from Redis DB.
        ///     
        /// </summary>
        /// <param name="key"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        public static object GetCache(string key, int diff, int dbNum, string customKey)
        {
            var redis = new RedisHelper(dbNum, customKey);

            // 从缓存读取
            var obj = redis.StringGet<T>(GetFullKey(key, diff));
            return obj;
        }

        /// <summary>
        ///     
        ///     @add by WuTianHui   2017-08-25
        ///     
        ///     Note: get info from Redis DB.
        ///     
        /// </summary>
        /// <param name="key"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        public static object GetCache(string key, Guid diff, int dbNum, string customKey)
        {
            var redis = new RedisHelper(dbNum, customKey);

            // 从缓存读取
            var obj = redis.StringGet<T>(GetFullKey(key, diff));
            return obj;
        }
    }
}