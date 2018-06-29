using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
   public class CacheHelper
    {
       /// <summary>
       /// 根据缓存的key，从缓存中取值.
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
       public static object Get(string key)
       {
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
           return cache[key];
       }
       /// <summary>
       /// 向缓存中添加数据
       /// </summary>
       /// <param name="key"></param>
       /// <param name="value"></param>
       public static void Set(string key, object value)
       {
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
           cache[key] = value;
       }

       /// <summary>
       /// 向缓存中添加数据
       /// </summary>
       /// <param name="key"></param>
       /// <param name="value"></param>
       public static void Set(string key, object value,DateTime time)
       {
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
           cache.Insert(key, value, null, time, TimeSpan.Zero);
       }
         /// <summary>
       /// 移除缓存中的数据
       /// </summary>
       /// <param name="key"></param>
       /// <param name="value"></param>
       public static void Reomve(string key)
       {
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
           cache.Remove(key);
       }

            
    }
}
