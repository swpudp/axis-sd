using AxisOrder.RepositoryContract;
using System.Threading.Tasks;

namespace AxisOrder.RepositoryImplement
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// 指定用户登录名判断是否存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public Task<bool> ExistUser(string loginName)
        {
            return Task.FromResult(true);
        }
    }
}
