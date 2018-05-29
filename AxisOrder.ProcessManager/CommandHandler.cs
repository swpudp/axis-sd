using Microsoft.Extensions.Configuration;
using Syllab.Configurations;
using Syllab.Mssql;
using System.Data;
using System.Data.SqlClient;

namespace AxisOrder.ProcessManager
{
    /// <summary>
    /// 通用处理程序
    /// </summary>
    public abstract class CommandHandler : AbstractHandler
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        protected override IDbConnection GetConnection()
        {
            return DbWriter;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string _dbWriter;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected static IDbConnection DbWriter => new SqlConnection(_dbWriter ?? (_dbWriter = ConfigManager.Configuration.GetConnectionString("WriteConnection")));
    }
}
