using AxisOrder.Models.Commands;
using AxisOrder.Models.Entities;
using AxisOrder.Models.Params;
using AxisOrder.Models.Views;
using AxisOrder.QueryContract;
using AxisOrder.WebApi.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Syllab;
using Syllab.Driver.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syllab.Extensions;

namespace AxisOrder.WebApi.Controllers
{
    /// <summary>
    /// 用户管理控制器
    /// </summary>
    [Produces("application/json")]
    [Route(ConstDefine.DefaultRouteTemplate)]
    [EnableCors(ConstDefine.DefaultCrosPolicy)]
    public class UserController : AuthorizedController
    {
        /// <summary>
        /// 命令总线
        /// </summary>
        private readonly ICommandBus _bus;


        /// <summary>
        /// 用户查询
        /// </summary>
        private readonly IUserQuery _userQuery;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="userQuery"></param>
        public UserController(ICommandBus bus, IUserQuery userQuery)
        {
            _bus = bus;
            _userQuery = userQuery;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                return Json(new Respond { IsSucceed = false, Message = string.Join(",", errors) });
            }
            var exist = await _userQuery.QueryByLoginAsync(userRegister.LoginName);
            if (exist != null)
            {
                return Json(new Respond { IsSucceed = false, Message = $"登录名{userRegister.LoginName}已存在!" });
            }
            userRegister.Id = NewId.StringId();
            userRegister.Password = userRegister.Password.CreatePassword();
            var cmd = new UserRegisterCommand(userRegister) { Id = userRegister.Id };
            await _bus.SendAsync(cmd);
            return Json(Respond.Succeed);
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UserUpdate userUpdate)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                return Json(new Respond { IsSucceed = false, Message = string.Join(",", errors) });
            }
            var cmd = new UserUpdateCommand(userUpdate) { Id = userUpdate.Id };
            var result = await _bus.ExecuteAsync(cmd);
            return Json(result);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpDelete("{id}_{version}")]
        public async Task<IActionResult> Delete(string id, int version)
        {
            var cmd = new UserDeleteCommand { Id = id, Version = version, UpdateTime = DateTime.Now };
            await _bus.SendAsync(cmd);
            return Json(Respond.Succeed);
        }

        /// <summary>
        /// 指定查询条件查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody]UserParam param)
        {
            var result = await _userQuery.QueryPaged(param);
            return Json(new { total = result.Total, rows = result.Results });
        }

    }
}