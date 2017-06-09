using Microsoft.Practices.Unity;
using System;
using System.ComponentModel;
using System.Web.Http.Dependencies;

namespace MvcBase.IoC
{
    public class WebapiUnityDependencyResolver : WebapiUnityDependencyScope, IDisposable, IDependencyResolver, IDependencyScope
    {

        public WebapiUnityDependencyResolver(IUnityContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            return (IDependencyScope)new WebapiUnityDependencyScope(this.Container.CreateChildContainer());
        }
    }
}
