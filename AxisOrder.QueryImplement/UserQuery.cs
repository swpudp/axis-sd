using AxisOrder.Common;
using AxisOrder.Models.Views;
using AxisOrder.QueryContract;
using Syllab.Components;
using Syllab.Mssql.Interpret;
using System.Threading.Tasks;

namespace AxisOrder.QueryImplement
{
    /// <summary>
    /// 用户查询
    /// </summary>
    [Component]
    public class UserQuery : AbstractGeneralQuery, IUserQuery
    {
        /// <summary>
        /// 指定登录名查询用户
        /// </summary>
        /// <param name="loginName">用户登录名</param>
        /// <returns></returns>
        public Task<UserView> QueryByLoginAsync(string loginName)
        {
            var sqlBuilder = InterpretSql.From(Tables.UserTable).Where("LoginName = @LoginName");
            return QuerySingleAsync<UserView>(sqlBuilder, new { LoginName = loginName });
        }
    }
}
