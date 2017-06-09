using Microsoft.Practices.Unity;
using System;
using System.ComponentModel;
using System.Web;

namespace MvcBase.IoC
{
    public class HttpContextLifetimeManager : LifetimeManager, IDisposable
    {
        private readonly Type type_0;


        public HttpContextLifetimeManager(Type type)
        {
            this.type_0 = type;
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[(object)this.type_0.AssemblyQualifiedName];
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Items.Remove((object)this.type_0.AssemblyQualifiedName);
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[(object)this.type_0.AssemblyQualifiedName] = newValue;
        }

        public void Dispose()
        {
            this.RemoveValue();
        }
    }
    public class HttpContextLifetimeManager<T> : HttpContextLifetimeManager
    {

        public HttpContextLifetimeManager()
            : base(typeof(T))
        {
        }
    }
}
