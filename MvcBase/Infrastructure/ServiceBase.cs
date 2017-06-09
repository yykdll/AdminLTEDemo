using MvcBase.Helper;
using MvcBase.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MvcBase.Infrastructure
{
    public abstract class ServiceBase<T> : Disposable, IServiceBase<T> where T : class
    {
        protected IDbSet<T> dbset { get; private set; }

        protected IDatabaseFactory DatabaseFactory { get; private set; }

        private FrameworkContext frameworkContext { get; set; }

        protected FrameworkContext DataContext
        {
            get
            {
                FrameworkContext frameworkContext1 = this.frameworkContext;
                if (frameworkContext1 != null)
                    return frameworkContext1;
                var frameworkContext2 = this.DatabaseFactory.Get();
                frameworkContext = frameworkContext2;
                return frameworkContext2;
            }
        }


        protected ServiceBase(IDatabaseFactory databaseFactory)
        {
            this.DatabaseFactory = databaseFactory;
            this.dbset = (IDbSet<T>)this.DataContext.Set<T>();
        }

        public virtual void Delete(params object[] id)
        {
            T entity = this.dbset.Find(id);
            if ((object)entity == null)
                return;
            // ISSUE: reference to a compiler-generated method
            this.DataContext.Entry<T>(entity).State = EntityState.Deleted;
            this.dbset.Remove(entity);
        }

        public virtual EntityDataLog DeleteAndLog(params object[] id)
        {
            EntityDataLog entityDataLog = (EntityDataLog)null;
            T entity = this.dbset.Find(id);
            if ((object)entity != null)
            {
                entityDataLog = this.Logs(entity);
                this.RemoveEntity(entity);
            }
            return entityDataLog;
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            foreach (T entity in this.ListWithTracking(where).AsEnumerable<T>())
                this.RemoveEntity(entity);
        }

        public virtual List<EntityDataLog> DeleteAndLogs(Expression<Func<T, bool>> where)
        {
            List<EntityDataLog> entityDataLogList = new List<EntityDataLog>();
            foreach (T entity in this.ListWithTracking(where).AsEnumerable<T>())
            {
                entityDataLogList.Add(this.Logs(entity));
                this.RemoveEntity(entity);
            }
            return entityDataLogList;
        }

        protected void RemoveEntity(T entity)
        {
            // ISSUE: reference to a compiler-generated method
            this.DataContext.Entry<T>(entity).State = EntityState.Deleted;
            this.dbset.Remove(entity);
        }

        public virtual T Single(params object[] id)
        {
            return this.dbset.Find(id);
        }

        public virtual T SingleAndInit(params object[] id)
        {
            T obj = this.dbset.Find(id);
            if ((object)obj == null)
                return Activator.CreateInstance<T>();
            return obj;
        }

        public virtual T Single(Expression<Func<T, bool>> where)
        {
            return this.dbset.Where<T>(where).FirstOrDefault<T>();
        }

        public virtual T SingleAndInit(Expression<Func<T, bool>> where)
        {
            T obj = this.dbset.Where<T>(where).FirstOrDefault<T>();
            if ((object)obj == null)
                return Activator.CreateInstance<T>();
            return obj;
        }

        public virtual IQueryable<T> List()
        {
            return this.dbset.AsNoTracking<T>();
        }

        public virtual IQueryable<T> List(Expression<Func<T, bool>> where)
        {
            return this.dbset.AsNoTracking<T>().Where<T>(where);
        }

        public virtual IQueryable<T> ListWithTracking()
        {
            return (IQueryable<T>)this.dbset;
        }

        public virtual IQueryable<T> ListWithTracking(Expression<Func<T, bool>> where)
        {
            return this.dbset.Where<T>(where);
        }

        public virtual IQueryable<T> ListInclude(params Expression<Func<T, object>>[] includes)
        {
            return ((IEnumerable<Expression<Func<T, object>>>)includes).Aggregate<Expression<Func<T, object>>, IQueryable<T>>(this.dbset.AsNoTracking<T>(), (Func<IQueryable<T>, Expression<Func<T, object>>, IQueryable<T>>)((current, include) => current.Include<T, object>(include)));
        }

        public virtual IQueryable<T> ListIncludeWithTracking(params Expression<Func<T, object>>[] includes)
        {
            return ((IEnumerable<Expression<Func<T, object>>>)includes).Aggregate<Expression<Func<T, object>>, IQueryable<T>>(this.dbset.AsQueryable<T>(), (Func<IQueryable<T>, Expression<Func<T, object>>, IQueryable<T>>)((current, include) => current.Include<T, object>(include)));
        }

        public virtual IQueryable<T> ListInclude(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            return ((IEnumerable<Expression<Func<T, object>>>)includes).Aggregate<Expression<Func<T, object>>, IQueryable<T>>(this.dbset.Where<T>(where).AsNoTracking<T>(), (Func<IQueryable<T>, Expression<Func<T, object>>, IQueryable<T>>)((current, include) => current.Include<T, object>(include)));
        }

        public virtual IQueryable<T> ListIncludeWithTracking(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            return ((IEnumerable<Expression<Func<T, object>>>)includes).Aggregate<Expression<Func<T, object>>, IQueryable<T>>(this.dbset.Where<T>(where), (Func<IQueryable<T>, Expression<Func<T, object>>, IQueryable<T>>)((current, include) => current.Include<T, object>(include)));
        }

        public virtual List<T> ListCache(string cacheName)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                CacheExtensions.SetCache(cacheName, (object)this.dbset.AsNoTracking<T>().ToList<T>());
            return CacheExtensions.GetCache<List<T>>(cacheName);
        }

        public virtual List<T> ListCache(string cacheName, Expression<Func<T, bool>> where)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                CacheExtensions.SetCache(cacheName, (object)this.dbset.AsNoTracking<T>().Where<T>(where).ToList<T>());
            return CacheExtensions.GetCache<List<T>>(cacheName);
        }

        public virtual List<T> ListCache(string cacheName, CacheTimeType cacheTimeType, int cacheTime)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                CacheExtensions.SetCache(cacheName, (object)this.dbset.AsNoTracking<T>().ToList<T>(), cacheTimeType, cacheTime);
            return CacheExtensions.GetCache<List<T>>(cacheName);
        }

        public virtual List<T> ListCache(string cacheName, CacheTimeType cacheTimeType, int cacheTime, int count)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                CacheExtensions.SetCache(cacheName, (object)this.dbset.AsNoTracking<T>().Take<T>(count).ToList<T>(), cacheTimeType, cacheTime);
            return CacheExtensions.GetCache<List<T>>(cacheName);
        }

        public virtual List<T> ListCache(string cacheName, CacheTimeType cacheTimeType, int cacheTime, Expression<Func<T, bool>> where)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                CacheExtensions.SetCache(cacheName, (object)this.dbset.AsNoTracking<T>().Where<T>(where).ToList<T>(), cacheTimeType, cacheTime);
            return CacheExtensions.GetCache<List<T>>(cacheName);
        }

        public virtual List<T> ListCache(string cacheName, CacheTimeType cacheTimeType, int cacheTime, Expression<Func<T, bool>> where, int count)
        {
            if (!CacheExtensions.CheckCache(cacheName))
                CacheExtensions.SetCache(cacheName, (object)this.dbset.AsNoTracking<T>().Where<T>(where).Take<T>(count).ToList<T>(), cacheTimeType, cacheTime);
            return CacheExtensions.GetCache<List<T>>(cacheName);
        }

        public void Save(T entity, params object[] id)
        {
            if ((object)this.Single(id) == null)
                this.Add(entity);
            else
                this.Update(entity);
        }

        public EntityDataLog SaveAndLog(T entity, params object[] id)
        {
            EntityDataLog entityDataLog = this.Logs(entity);
            this.Save(entity, id);
            return entityDataLog;
        }

        public void Add(T entity)
        {
            // ISSUE: reference to a compiler-generated method
            this.DataContext.Entry<T>(entity).State = EntityState.Added;
            this.dbset.Add(entity);
        }

        public void Add(List<T> entity)
        {
            try
            {
                // ISSUE: reference to a compiler-generated method
                this.DataContext.Configuration.AutoDetectChangesEnabled = false;
                // ISSUE: reference to a compiler-generated method
                this.DataContext.Configuration.ValidateOnSaveEnabled = false;
                foreach (T entity1 in entity)
                {
                    // ISSUE: reference to a compiler-generated method
                    this.DataContext.Entry<T>(entity1).State = EntityState.Added;
                    this.dbset.Add(entity1);
                }
            }
            finally
            {
                // ISSUE: reference to a compiler-generated method
                this.DataContext.Configuration.AutoDetectChangesEnabled = true;
                // ISSUE: reference to a compiler-generated method
                this.DataContext.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public void Update(T entity)
        {
            this.dbset.Attach(entity);
            // ISSUE: reference to a compiler-generated method
            this.DataContext.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Update(T entity, Dictionary<string, string> updateKey, string where)
        {
            StringBuilder stringBuilder = new StringBuilder();
            object[] objArray = new object[updateKey.Count + 1];
            stringBuilder.Append("update " + typeof(T).Name + " set ");
            int index = 0;
            foreach (KeyValuePair<string, string> keyValuePair in updateKey)
            {
                stringBuilder.Append(keyValuePair.Key + "=@p" + (object)index);
                if (index < updateKey.Count - 1)
                    stringBuilder.Append(",");
                else
                    stringBuilder.Append(" ");
                objArray[index] = (object)keyValuePair.Value;
                ++index;
            }
            stringBuilder.Append(" where " + where);
            // ISSUE: reference to a compiler-generated method
            this.DataContext.Database.ExecuteSqlCommand(stringBuilder.ToString(), objArray);
        }

        public EntityDataLog Logs(T entity)
        {
            EntityDataLog entityDataLog = new EntityDataLog();
            Type type = typeof(T);
            entityDataLog.FullName = type.FullName;
            DisplayNameAttribute[] customAttributes1 = ReflectionExtensions.GetCustomAttributes<DisplayNameAttribute>(type);
            DisplayAttribute[] customAttributes2 = ReflectionExtensions.GetCustomAttributes<DisplayAttribute>(type);
            if (customAttributes1 != null && customAttributes1.Length > 0)
                entityDataLog.DisplayName = customAttributes1[0].DisplayName;
            else if (customAttributes2 != null && customAttributes2.Length > 0)
                entityDataLog.DisplayName = customAttributes2[0].Name;
            // ISSUE: reference to a compiler-generated method
            DbEntityEntry<T> dbEntityEntry = this.DataContext.Entry<T>(entity);
            if (dbEntityEntry.State == EntityState.Modified)
            {
                foreach (string propertyName in dbEntityEntry.OriginalValues.PropertyNames)
                {
                    EntityPropertyData entityPropertyData = new EntityPropertyData();
                    entityPropertyData.Name = propertyName;
                    DisplayNameAttribute displayAttribute1 = ClassExtensions<T>.GetDisplayAttribute<DisplayNameAttribute>(propertyName);
                    DisplayAttribute displayAttribute2 = ClassExtensions<T>.GetDisplayAttribute(propertyName);
                    if (!string.IsNullOrEmpty(displayAttribute1.DisplayName))
                        entityPropertyData.DisplayName = displayAttribute1.DisplayName;
                    else if (!string.IsNullOrEmpty(displayAttribute2.Name))
                        entityPropertyData.DisplayName = displayAttribute2.Name;
                    entityPropertyData.OldValue = dbEntityEntry.OriginalValues[propertyName];
                    entityPropertyData.NewValue = dbEntityEntry.CurrentValues[propertyName];
                    entityDataLog.ChangedProperties.Add(entityPropertyData);
                }
            }
            else
            {
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    EntityPropertyData entityPropertyData = new EntityPropertyData();
                    entityPropertyData.Name = property.Name;
                    DisplayNameAttribute displayAttribute1 = ClassExtensions<T>.GetDisplayAttribute<DisplayNameAttribute>(entityPropertyData.Name);
                    DisplayAttribute displayAttribute2 = ClassExtensions<T>.GetDisplayAttribute(entityPropertyData.Name);
                    if (!string.IsNullOrEmpty(displayAttribute1.DisplayName))
                        entityPropertyData.DisplayName = displayAttribute1.DisplayName;
                    else if (!string.IsNullOrEmpty(displayAttribute2.Name))
                        entityPropertyData.DisplayName = displayAttribute2.Name;
                    entityPropertyData.OldValue = property.GetValue((object)entity, (object[])null);
                    entityDataLog.ChangedProperties.Add(entityPropertyData);
                }
            }
            return entityDataLog;
        }
    }
}