using System;
using System.ComponentModel;

namespace MvcBase.Infrastructure
{
    public class Disposable : IDisposable
    {
        private bool bool_0;

        ~Disposable()
        {
            this.method_0(false);
        }

        public void Dispose()
        {
            this.method_0(true);
            GC.SuppressFinalize((object)this);
        }

        private void method_0(bool bool_1)
        {
            if (!this.bool_0 && bool_1)
                this.DisposeCore();
            this.bool_0 = true;
        }

        protected virtual void DisposeCore()
        {
        }
    }
}

