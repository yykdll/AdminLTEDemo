using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;

namespace MvcBase.IoC
{
    public class WebapiUnityDependencyScope : IDisposable, IDependencyScope
    {
        protected IUnityContainer Container { get; private set; }


        public WebapiUnityDependencyScope(IUnityContainer container)
        {
            this.Container = container;
        }

        public object GetService(Type serviceType)
        {
            if (typeof(IHttpController).IsAssignableFrom(serviceType))
                return this.Container.Resolve(serviceType, new ResolverOverride[0]);
            try
            {
                return this.Container.Resolve(serviceType, new ResolverOverride[0]);
            }
            catch
            {
                return (object)null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.Container.ResolveAll(serviceType);
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}
