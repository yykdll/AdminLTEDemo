using MvcBase;
using MvcBase.Infrastructure;
using MvcBase.Service;
using System.Data.Entity;
using AdminLTE.Domain;


namespace AdminLTE
{
    public class CustomDbTestFactory : DatabaseFactory<MainDbTestContext>, IMainDbFactory
    {

    }
}
namespace AdminLTE.Domain
{
    public partial class MainDbTestContext : FrameworkContext
    {
        public MainDbTestContext() : base("DefaultConnectionTest") { }

        static MainDbTestContext()
        {
            Database.SetInitializer(new NullDatabaseInitializer<MainDbTestContext>());//禁用实体映射表自动改动数据结构功能
        }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    public partial class MainDbTestContext
    {
        public DbSet<Menu> Menu { get; set; }
    }

    namespace Services
    {
        public interface IMainDBTestTool : IDBTool { }
        public class MainDBTestTool : DBTool, IMainDBTestTool
        {
            public MainDBTestTool(IMainDbFactory DbFactory) : base(DbFactory) { }
        }
    }
}
