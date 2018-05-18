using Microsoft.Extensions.Configuration;
using Syllab.Configurations;
using Syllab.Data;
using Syllab.Mssql;
using System.Data;
using System.Data.SqlClient;

namespace AxisOrder.QueryImplement
{
    /// <summary>
    /// 抽象通用查询
    /// </summary>
    public abstract class AbstractGeneralQuery : AbstractQuery<string>
    {
        /// <summary>
        /// 读库数据库连接
        /// </summary>
        private static string ReadConnection => ConfigManager.Configuration.GetConnectionString("ReadConnection");

        /// <summary>
        /// 获取数据连接
        /// </summary>
        /// <returns></returns>
        protected override IDbConnection GetConnection()
        {
            return new SqlConnection(ReadConnection);
        }
    }
}
