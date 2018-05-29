using AxisOrder.Common;
using AxisOrder.Models.Params;
using AxisOrder.Models.Views;
using AxisOrder.QueryContract;
using Dapper;
using Syllab;
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
        /// 分页查询
        /// </summary>
        /// <param name="userParam"></param>
        /// <returns></returns>
        public Task<PagedResult<UserView>> QueryPaged(UserParam userParam)
        {
            var queryParam = new DynamicParameters();
            queryParam.Add("IsDelete", false);
            var builder = InterpretSql.From(Tables.UserTable).Where("IsDelete = @IsDelete");
            if (!string.IsNullOrEmpty(userParam.LoginName))
            {
                builder.Where("LoginName like @LoginName");
                queryParam.Add("LoginName", "%" + userParam.LoginName + "%");
            }
            if (!string.IsNullOrEmpty(userParam.FullName))
            {
                builder.Where("FullName like @FullName");
                queryParam.Add("FullName", "%" + userParam.FullName + "%");
            }
            return QueryPagedAsync<UserView>(builder, queryParam, userParam.PageIndex, userParam.PageSize);
        }

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
