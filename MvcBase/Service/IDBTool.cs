using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;

namespace MvcBase.Service
{
    public interface IDBTool
    {
        List<T> Execute<T>(string storedProcedure, DbParameter[] sqlParameter);

        int ExecutSql(string sql, DbParameter[] sqlParameter);

        void Commit();

        DbConnection GetConnection();
    }
}
