using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            if (!condition)
                return source;
            return source.Where<T>(predicate);
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, int, bool>> predicate, bool condition)
        {
            if (!condition)
                return source;
            return source.Where<T>(predicate);
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            if (!condition)
                return source;
            return source.Where<T>(predicate);
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> predicate, bool condition)
        {
            if (!condition)
                return source;
            return source.Where<T>(predicate);
        }

        public static void SyncProperty<T>(T source, T destination) where T : class
        {
            foreach (PropertyInfo property in source.GetType().GetProperties())
                property.SetValue((object)destination, property.GetValue((object)source, (object[])null), (object[])null);
        }

        public static DataTable CopyToDataTable<T>(this IEnumerable<T> array)
        {
            DataTable dataTable = new DataTable();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(typeof(T)))
                dataTable.Columns.Add(property.Name, property.PropertyType);
            foreach (T obj in array)
            {
                DataRow row = dataTable.NewRow();
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(typeof(T)))
                    row[property.Name] = property.GetValue((object)obj);
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public static T SingleAndInit<T>(this List<T> source, Func<T, bool> predicate)
        {
            T obj = source.FirstOrDefault<T>(predicate);
            if ((object)obj == null)
                return Activator.CreateInstance<T>();
            return obj;
        }

        public static T AutoInit<T>(this T source)
        {
            if ((object)source == null)
                return Activator.CreateInstance<T>();
            return source;
        }
    }
}
