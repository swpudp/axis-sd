using AxisOrder.Common;
using AxisOrder.Models.Params;
using AxisOrder.Models.Views;
using AxisOrder.QueryContract;
using Dapper;
using Syllab;
using Syllab.Components;
using Syllab.Mssql.Interpret;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxisOrder.QueryImplement
{
    /// <summary>
    /// 用户查询
    /// </summary>
    [Component]
    public class UserQuery : AbstractGeneralQuery, IUserQuery
    {
        private static readonly Dictionary<string, UserView> keyValuePairs = new Dictionary<string, UserView>();

        private static IList<UserView> userViews = new List<UserView>();

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
        public async Task<UserView> QueryByLoginAsync(string loginName)
        {
            if (userViews.Any(x => x.LoginName == loginName))
            {
                return userViews.First(x => x.LoginName == loginName);
            }
            var sqlBuilder = InterpretSql.From(Tables.UserTable).Where("LoginName = @LoginName");
            var user = await QuerySingleAsync<UserView>(sqlBuilder, new { LoginName = loginName });
            if (user == null) return null;
            userViews.Add(user);
            return user;
        }
    }
}
