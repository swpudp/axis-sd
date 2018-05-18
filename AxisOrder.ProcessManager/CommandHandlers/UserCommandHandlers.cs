using AxisOrder.Models.Commands;
using Syllab;
using Syllab.Components;
using Syllab.Driver.Commanding;
using System.Threading.Tasks;

namespace AxisOrder.ProcessManager.CommandHandlers
{
    [Component]
    public class UserCommandHandlers :
        ICommandHandlerAsync<UserRegisterCommand>//用户注册命令处理
    {
        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Task<Respond> HandleAsync(UserRegisterCommand c)
        {
            return Task.FromResult(new Respond { IsSucceed = true, Message = $"{nameof(c.Id)}={c.Id},{nameof(c.UserRegister)}={c.UserRegister.Id}" });
        }
    }
}
