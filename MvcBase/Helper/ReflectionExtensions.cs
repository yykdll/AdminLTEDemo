using System;
using System.ComponentModel;

namespace MvcBase.Helper
{
    public static class ReflectionExtensions
    {
        public static T[] GetCustomAttributes<T>(this Type type) where T : Attribute
        {
            return new T[0];
        }
    }
}
