using AxisOrder.Models.Entities;
using Syllab.Driver.Commanding;

namespace AxisOrder.Models.Commands
{
    /// <summary>
    /// 用户注册命令
    /// </summary>
    public class UserRegisterCommand : VersionCommand
    {
        /// <summary>
        /// 用户注册实体
        /// </summary>
        public UserRegisterEntity UserRegister { get; private set; }

        public UserRegisterCommand(string id, UserRegisterEntity userRegisterEntity)
        {
            UserRegister = userRegisterEntity;
            Id = id;
        }
    }
}
