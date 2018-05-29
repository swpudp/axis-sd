using AxisOrder.Common;
using AxisOrder.Models.Commands;
using Syllab;
using Syllab.Components;
using Syllab.Driver.Commanding;
using Syllab.Mssql;
using System.Threading.Tasks;

namespace AxisOrder.ProcessManager.CommandHandlers
{
    [Component]
    public class UserCommandHandlers : CommandHandler
        , ICommandHandlerAsync<UserRegisterCommand>
        , ICommandHandlerAsync<UserUpdateCommand>
        , ICommandHandler<UserDeleteCommand>//用户注册命令处理
    {
        public Task Handle(UserDeleteCommand c)
        {
            return TryUpdateAsync(conn => conn.UpdateAsync(new { IsDelete = true, c.Updator, c.UpdateTime, Version = c.Version + 1 }, new { c.Id, c.Version }, Tables.UserTable));
        }

        public Task<Respond> HandleAsync(UserUpdateCommand c)
        {
            var ver = c.Version;
            c.Version = ver + 1;
            return TryUpdateAsync(conn => conn.UpdateAsync(c.UserUpdate, new { c.Id, ver }, Tables.UserTable));
        }

        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public async Task<Respond> HandleAsync(UserRegisterCommand c)
        {
            var userExist = await ExecuteAsync(conn => conn.GetCountAsync(new { c.UserRegister.LoginName, IsDelete = false }, Tables.UserTable));
            if (userExist > 0)
            {
                return new Respond { IsSucceed = false, Message = $"登录名{c.UserRegister.LoginName}已存在!" };
            }
            return await TryCreateAsync(conn => conn.InsertAsync(c.UserRegister, Tables.UserTable));
        }
    }
}
