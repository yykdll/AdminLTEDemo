using System;
using System.ComponentModel;

namespace MvcBase.Infrastructure
{
    public class DatabaseFactory<TContext> : Disposable, IDisposable, IDatabaseFactory where TContext : FrameworkContext
    {
        private FrameworkContext frameworkContext_0;


        public virtual FrameworkContext Get()
        {
            return this.frameworkContext_0 ?? (this.frameworkContext_0 = (FrameworkContext)Activator.CreateInstance<TContext>());
        }

        protected override void DisposeCore()
        {
            if (this.frameworkContext_0 == null)
                return;
            this.frameworkContext_0.Dispose();
        }
    }
}

