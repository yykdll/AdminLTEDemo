using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System
{
    public static class ClassExtensions<T> where T : class
    {

        public static DisplayAttribute GetDisplayAttribute(string propertyName)
        {
            return ClassExtensions<T>.GetDisplayAttribute<DisplayAttribute>(propertyName);
        }

        public static TSource GetDisplayAttribute<TSource>(string propertyName) where TSource : new()
        {
            PropertyInfo propertyInfo = ((IEnumerable<PropertyInfo>)typeof(T).GetProperties()).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(s => s.Name == propertyName));
            if (propertyInfo == (PropertyInfo)null)
                return new TSource();
            object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true);
            if (customAttributes.Length > 0)
                return (TSource)customAttributes[0];
            return new TSource();
        }

        public static string GetPropertyName(Expression<Func<T, object>> expr)
        {
            string str = "";
            if (expr.Body is UnaryExpression)
                str = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            else if (expr.Body is MemberExpression)
                str = ((MemberExpression)expr.Body).Member.Name;
            else if (expr.Body is ParameterExpression)
                str = expr.Body.Type.Name;
            return str;
        }
    }
}

