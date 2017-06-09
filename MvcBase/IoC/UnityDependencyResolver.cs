// Decompiled with JetBrains decompiler
// Type: MvcCore.IoC.UnityDependencyResolver
// Assembly: MvcCore, Version=1.0.0.30, Culture=neutral, PublicKeyToken=null
// MVID: 2E513CDA-5999-4C86-8B0D-323ED256A8F6
// Assembly location: F:\反混淆\MvcCore-cleaned.dll

using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http.Dependencies;

namespace MvcBase.IoC
{
    public class UnityDependencyResolver : IDisposable, System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver, IDependencyScope
    {
        private IUnityContainer iunityContainer_0;


        public UnityDependencyResolver(IUnityContainer container)
        {
            this.iunityContainer_0 = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.iunityContainer_0.Resolve(serviceType, new ResolverOverride[0]);
            }
            catch
            {
                return (object)null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.iunityContainer_0.ResolveAll(serviceType);
            }
            catch
            {
                return (IEnumerable<object>)new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            return (IDependencyScope)new UnityDependencyResolver(this.iunityContainer_0.CreateChildContainer());
        }

        public void Dispose()
        {
            this.iunityContainer_0.Dispose();
        }
    }
}
