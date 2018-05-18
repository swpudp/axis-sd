using AxisOrder.Models.Views;
using Syllab.Data;
using System.Threading.Tasks;

namespace AxisOrder.QueryContract
{
    /// <summary>
    /// 用户查询接口
    /// </summary>
    public interface IUserQuery : IQuery<string>
    {
        /// <summary>
        /// 指定登录查询用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        Task<UserView> QueryByLoginAsync(string loginName);
    }
}
