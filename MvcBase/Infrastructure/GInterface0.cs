using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace MvcBase.Infrastructure
{
    public interface GInterface0<T> where T : class
    {
        void Mapping(EntityTypeConfiguration<T> Entity);
    }
}
