using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcBase
{
  public class FrameworkContext : DbContext
  {

    public FrameworkContext():base("ConnectionString")
    {
      
    }

    public FrameworkContext(string connection):base(connection)
    {
        
    }

    public virtual void Commit()
    {
      this.SaveChanges();
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("STUDY");
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        modelBuilder.Conventions.Remove<IdKeyDiscoveryConvention>();
    }
  }
}
