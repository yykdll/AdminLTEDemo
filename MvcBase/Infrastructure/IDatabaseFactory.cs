using System;
using System.ComponentModel;

namespace MvcBase.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        FrameworkContext Get();
    }
}