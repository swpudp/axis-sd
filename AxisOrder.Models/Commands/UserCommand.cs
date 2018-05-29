using AxisOrder.Models.Entities;
using Syllab.Driver.Commanding;
using System;

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
        public UserRegister UserRegister { get; private set; }

        public UserRegisterCommand(UserRegister userRegisterEntity)
        {
            UserRegister = userRegisterEntity;
        }
    }


    /// <summary>
    /// 用户更新命令
    /// </summary>
    public class UserUpdateCommand : VersionCommand
    {
        /// <summary>
        /// 用户更新实体
        /// </summary>
        public UserUpdate UserUpdate { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userUpdate"></param>
        public UserUpdateCommand(UserUpdate userUpdate)
        {
            UserUpdate = userUpdate;
        }
    }

    /// <summary>
    /// 用户删除命令
    /// </summary>
    public class UserDeleteCommand : VersionCommand
    {
        /// <summary>
        /// 更新人
        /// </summary>
        public string Updator { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
