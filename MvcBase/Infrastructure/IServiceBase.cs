using MvcBase.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace MvcBase.Infrastructure
{
    public interface IServiceBase<T> where T : class
    {
        void Save(T entity, params object[] id);

        EntityDataLog SaveAndLog(T entity, params object[] id);

        void Add(T entity);

        void Add(List<T> entity);

        void Update(T entity);

        void Update(T entity, Dictionary<string, string> updateKey, string where);

        void Delete(params object[] id);

        EntityDataLog DeleteAndLog(params object[] id);

        void Delete(Expression<Func<T, bool>> where);

        List<EntityDataLog> DeleteAndLogs(Expression<Func<T, bool>> where);

        T Single(params object[] id);

        T SingleAndInit(params object[] id);

        T Single(Expression<Func<T, bool>> where);

        T SingleAndInit(Expression<Func<T, bool>> where);

        List<T> ListCache(string cacheName);

        List<T> ListCache(string cacheName, Expression<Func<T, bool>> where);

        IQueryable<T> List();

        IQueryable<T> List(Expression<Func<T, bool>> where);

        IQueryable<T> ListWithTracking();

        IQueryable<T> ListWithTracking(Expression<Func<T, bool>> where);

        IQueryable<T> ListInclude(params Expression<Func<T, object>>[] includes);

        IQueryable<T> ListIncludeWithTracking(params Expression<Func<T, object>>[] includes);

        IQueryable<T> ListInclude(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        IQueryable<T> ListIncludeWithTracking(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        List<T> ListCache(string cacheName, CacheTimeType cacheTimeType, int cacheTime);

        List<T> ListCache(string cacheName, CacheTimeType cacheTimeType, int cacheTime, int count);

        List<T> ListCache(string cacheName, CacheTimeType cacheTimeType, int cacheTime, Expression<Func<T, bool>> where);

        List<T> ListCache(string cacheName, CacheTimeType cacheTimeType, int cacheTime, Expression<Func<T, bool>> where, int count);

        EntityDataLog Logs(T entity);
    }
}
