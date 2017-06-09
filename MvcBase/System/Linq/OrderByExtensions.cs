using MvcBase.Helper;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using MvcBase.Enum;

namespace System.Linq
{
    public static class OrderByExtensions
    {

        public static IOrderedQueryable<T> OrderIf<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> order, OrderByStatus status, bool isEnable)
        {
            IOrderedQueryable<T> orderedQueryable = (IOrderedQueryable<T>)null;
            if (source != null && order != null)
                orderedQueryable = !isEnable ? source as IOrderedQueryable<T> : (!source.ContainsOrderBy<T>() ? (status != OrderByStatus.ASC ? source.OrderByDescending<T, TKey>(order) : source.OrderBy<T, TKey>(order)) : (status != OrderByStatus.ASC ? (source as IOrderedQueryable<T>).ThenByDescending<T, TKey>(order) : (source as IOrderedQueryable<T>).ThenBy<T, TKey>(order)));
            return orderedQueryable;
        }

        public static bool ContainsOrderBy<T>(this IQueryable<T> source)
        {
            bool flag = false;
            Expression expression = source.Expression;
            if (expression.NodeType == ExpressionType.Call)
                flag = OrderByExtensions.smethod_0(expression as MethodCallExpression);
            return flag;
        }

        private static bool smethod_0(MethodCallExpression methodCallExpression_0)
        {
            bool flag = false;
            if (!(methodCallExpression_0.Method.Name == "OrderBy") && !(methodCallExpression_0.Method.Name == "OrderByDescending"))
            {
                foreach (Expression expression in methodCallExpression_0.Arguments)
                {
                    if (expression.NodeType == ExpressionType.Call)
                    {
                        if (flag = OrderByExtensions.smethod_0(expression as MethodCallExpression))
                            break;
                    }
                }
            }
            else
                flag = true;
            return flag;
        }

        private static IQueryable<T> smethod_1<T>(IQueryable<T> list, string string_0, string string_1) where T : new()
        {
            if (list == null)
                return list;
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(string_0);
            ParameterExpression parameterExpression = Expression.Parameter(type, "model");
            Expression body = (Expression)Expression.Property((Expression)parameterExpression, property);
            Expression expression1 = list.AsQueryable<T>().Expression;
            Type propertyType = property.PropertyType;
            Expression expression2 = (Expression)Expression.Call(typeof(Queryable), string_1, new Type[2]
      {
        type,
        propertyType
      }, new Expression[2]
      {
        expression1,
        (Expression) Expression.Lambda(body, new ParameterExpression[1]
        {
          parameterExpression
        })
      });
            return list.AsQueryable<T>().Provider.CreateQuery<T>(expression2);
        }

        public static IQueryable<T> OrderInfo<T>(this IQueryable<T> list, Dictionary<string, OrderByStatus> orderInfo) where T : new()
        {
            if (orderInfo != null && orderInfo.Count > 0)
            {
                string[] strArray = new string[4]
        {
          "OrderBy",
          "OrderByDescending",
          "ThenBy",
          "ThenByDescending"
        };
                bool flag = list.ContainsOrderBy<T>();
                foreach (string key in orderInfo.Keys)
                {
                    int index = orderInfo[key] == OrderByStatus.DESC ? 1 : 0;
                    if (flag)
                        index += 2;
                    list = OrderByExtensions.smethod_1<T>(list, key, strArray[index]);
                    flag = true;
                }
            }
            return list;
        }

        public static IEnumerable<T> OrderInfo<T>(this IEnumerable<T> list, Dictionary<string, OrderByStatus> orderInfo) where T : new()
        {
            return OrderByExtensions.OrderInfo<T>(list.AsQueryable<T>(), orderInfo).AsEnumerable<T>();
        }
    }
}

