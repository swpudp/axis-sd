using AxisOrder.Models.Params;
using AxisOrder.Models.Views;
using AxisOrder.WebApi.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Syllab;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxisOrder.WebApi.Controllers
{
    /// <summary>
    /// 用户管理控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors(ConstDefine.DefaultCrosPolicy)]
    public class UserController : AuthorizedController
    {

        private static IList<UserView> _users = Enumerable.Range(0, 1000).Select((x, idx) => new UserView
        {
            Id = NewId.StringId(),
            FullName = RandomProvider.RandomString(4),
            LoginName = RandomProvider.RandomString(6),
            IdNo = RandomProvider.RandomString(18, true),
            Mobile = RandomProvider.RandomString(11, true),
            Version = idx
        }).ToList();

        /// <summary>
        /// 指定查询条件查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody]BaseParam param)
        {
            var result = _users.Skip((param.Current - 1) * param.PageSize).Take(param.PageSize).ToList();
            return await Task.FromResult(Json(new { total = _users.Count, rows = result }));
        }

    }
}