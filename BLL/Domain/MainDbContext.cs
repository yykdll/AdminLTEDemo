using MvcBase;
using MvcBase.Infrastructure;
using MvcBase.Service;
using System.Data.Entity;
using AdminLTE.Domain;


namespace AdminLTE
{
    public interface IMainDbFactory : IDatabaseFactory
    {

    }
    public class CustomDbFactory : DatabaseFactory<MainDbContext>, IMainDbFactory
    {

    }
}
namespace AdminLTE.Domain
{
    public partial class MainDbContext : FrameworkContext
    {
        public MainDbContext() : base("DefaultConnection") { }

        static MainDbContext()
        {
            Database.SetInitializer(new NullDatabaseInitializer<MainDbContext>());//禁用实体映射表自动改动数据结构功能
        }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    namespace Services
    {
        public interface IMainDBTool : IDBTool { }
        public class MainDBTool : DBTool, IMainDBTool
        {
            public MainDBTool(IMainDbFactory DbFactory) : base(DbFactory) { }
        }
    }
}
