using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using MvcBase.Enum;

namespace MvcBase.Helper
{
    public static class CacheExtensions
    {

        public static MvcHtmlString Cache(this HtmlHelper htmlHelper, string cacheName, Func<object> func, CacheTimeType cacheTimeType, int cacheTime)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                CacheExtensions.SetCache(cacheName, (object)func().ToString(), cacheTimeType, cacheTime);
            return MvcHtmlString.Create(CacheExtensions.GetCache<string>(cacheName));
        }

        public static bool CheckCache(string cacheName)
        {
            return HttpContext.Current.Cache[cacheName.ToString()] != null;
        }

        public static T GetCache<T>(string cacheName)
        {
            return (T)HttpContext.Current.Cache[cacheName];
        }

        public static T GetCacheWithSet<T>(string cacheName, Func<T> valueFunc, CacheTimeType cacheTimeType, int times)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                CacheExtensions.SetCache(cacheName, (object)valueFunc(), cacheTimeType, times);
            return CacheExtensions.GetCache<T>(cacheName);
        }

        public static T GetCacheWithSet<T>(string cacheName, Func<T> valueFunc)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                CacheExtensions.SetCache(cacheName, (object)valueFunc());
            return CacheExtensions.GetCache<T>(cacheName);
        }

        public static List<T> GetCacheList<T>(string cacheName)
        {
            return (List<T>)HttpContext.Current.Cache[cacheName];
        }

        public static void SetCache(string cacheName, object value, CacheTimeType cacheTimeType, int cacheTime)
        {
            switch (cacheTimeType)
            {
                case CacheTimeType.ByMinutes:
                    HttpContext.Current.Cache.Insert(cacheName, value, (CacheDependency)null, DateTime.Now.AddMinutes((double)cacheTime), TimeSpan.Zero);
                    break;
                case CacheTimeType.ByHours:
                    HttpContext.Current.Cache.Insert(cacheName, value, (CacheDependency)null, DateTime.Now.AddHours((double)cacheTime), TimeSpan.Zero);
                    break;
                case CacheTimeType.ByDays:
                    HttpContext.Current.Cache.Insert(cacheName, value, (CacheDependency)null, DateTime.Now.AddDays((double)cacheTime), TimeSpan.Zero);
                    break;
                case CacheTimeType.ByYears:
                    HttpContext.Current.Cache.Insert(cacheName, value, (CacheDependency)null, DateTime.Now.AddYears(cacheTime), TimeSpan.Zero);
                    break;
            }
        }

        public static void SetCache(string cacheName, object value)
        {
            HttpContext.Current.Cache.Insert(cacheName, value);
        }

        public static List<string> GetAllCache()
        {
            List<string> stringList = new List<string>();
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
            while (enumerator.MoveNext())
                stringList.Add(enumerator.Key.ToString());
            return stringList;
        }

        public static void ClearCache(string cacheName)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                return;
            HttpContext.Current.Cache.Remove(cacheName);
        }

        public static void RestCache(string cacheName, object value)
        {
            if (CacheExtensions.CheckCache(cacheName))
                HttpContext.Current.Cache[cacheName] = value;
            else
                CacheExtensions.SetCache(cacheName, value);
        }
    }
}

