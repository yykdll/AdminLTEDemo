using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace MvcBase
{
    public static class FrameworkContextHelper
    {

        public static void Mapping(this DbModelBuilder modelBuilder, string assemblyString, string mappingNamespace)
        {
            Assembly.Load(assemblyString).DefinedTypes.Where<TypeInfo>((Func<TypeInfo, bool>)(s =>
            {
                if (mappingNamespace.Contains(s.Namespace) && s.IsClass)
                    return s.ImplementedInterfaces.Any();
                return false;
            })).ToList<TypeInfo>();
        }
    }
}
