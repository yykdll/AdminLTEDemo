using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcBase.IoC;

namespace MvcBase
{
    public class Unity
    {
        private static IUnityContainer iunityContainer_0;

        public static event Action<IUnityContainer> RegisterTypesEvent;

        public static IUnityContainer Get()
        {
            if (MvcBase.Unity.iunityContainer_0 == null)
                MvcBase.Unity.Create();
            return MvcBase.Unity.iunityContainer_0;
        }

        private static void Create()
        {
            MvcBase.Unity.iunityContainer_0 = (IUnityContainer)new UnityContainer();
            MvcBase.Unity.iunityContainer_0.RegisterType<IControllerFactory, UnityControllerFactory>((LifetimeManager)new HttpContextLifetimeManager<IControllerFactory>(), new InjectionMember[0]);
            if (MvcBase.Unity.RegisterTypesEvent == null)
                return;
            MvcBase.Unity.RegisterTypesEvent(MvcBase.Unity.iunityContainer_0);
        }

        public static T Get<T>()
        {
            return MvcBase.Unity.Get().Resolve<T>();
        }

        public static IEnumerable<T> GetAll<T>()
        {
            return MvcBase.Unity.Get().ResolveAll<T>();
        }
    }
}
