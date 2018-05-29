using System.Threading.Tasks;

namespace AxisOrder.RepositoryContract
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 指定用户登录名判断是否存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        Task<bool> ExistUser(string loginName);
    }
}
