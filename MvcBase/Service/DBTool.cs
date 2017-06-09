using MvcBase.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MvcBase.Service
{
    public class DBTool : IDBTool
    {
        protected readonly IDatabaseFactory databaseFactory;
        protected FrameworkContext dataContext;

        protected FrameworkContext DataContext
        {
            get
            {
                return this.dataContext ?? (this.dataContext = this.databaseFactory.Get());
            }
        }

        public DBTool(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public List<T> Execute<T>(string storedProcedure, DbParameter[] sqlParameter)
        {
            return this.DataContext.Database.SqlQuery<T>(storedProcedure, (object[])sqlParameter).ToList<T>();
        }

        public int ExecutSql(string sql, DbParameter[] param)
        {
            return this.DataContext.Database.ExecuteSqlCommand(sql, (object[])param);
        }

        public void Commit()
        {
            try
            {
                this.DataContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public DbConnection GetConnection()
        {
            return this.DataContext.Database.Connection;
        }
    }
}
