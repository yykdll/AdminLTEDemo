using Microsoft.Practices.Unity;
using MvcBase.Infrastructure;
using MvcBase.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcBase
{
    public static class UnityHelper
    {
        public static void Init(Action<IUnityContainer> RegistTypes)
        {
            MvcBase.Unity.RegisterTypesEvent += RegistTypes;
            UnityDependencyResolver dependencyResolver = new UnityDependencyResolver(MvcBase.Unity.Get());
            DependencyResolver.SetResolver((System.Web.Mvc.IDependencyResolver)dependencyResolver);
            GlobalConfiguration.Configuration.DependencyResolver = (System.Web.Http.Dependencies.IDependencyResolver)dependencyResolver;
        }

        public static void RequestEnd<TIDatabaseFactory>() where TIDatabaseFactory : IDatabaseFactory
        {
            new HttpContextLifetimeManager<TIDatabaseFactory>().Dispose();
        }

        public static IUnityContainer Bind<TFrom, TTo>(this IUnityContainer container) where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>((LifetimeManager)new HttpContextLifetimeManager<TFrom>(), new InjectionMember[0]);
        }

        public static IUnityContainer Bind(this IUnityContainer container, Type InterfaceType, Type classType)
        {
            return container.RegisterType(InterfaceType, classType, (LifetimeManager)new HttpContextLifetimeManager(InterfaceType), new InjectionMember[0]);
        }

        public static IUnityContainer LoadAssemblyAndBind(this IUnityContainer container, string assemblyString, params string[] namespances)
        {
            foreach (TypeInfo typeInfo in Assembly.Load(assemblyString).DefinedTypes.Where<TypeInfo>((Func<TypeInfo, bool>)(s =>
            {
                if (((IEnumerable<string>)namespances).Contains<string>(s.Namespace) && s.IsClass)
                    return s.ImplementedInterfaces.Count<Type>() > 0;
                return false;
            })).ToList<TypeInfo>())
            {
                Type InterfaceType = typeInfo.ImplementedInterfaces.ToArray<Type>()[typeInfo.ImplementedInterfaces.Count<Type>() - 1];
                if (InterfaceType != (Type)null)
                    container.Bind(InterfaceType, (Type)typeInfo);
            }
            return container;
        }
    }
}
