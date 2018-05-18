using AxisOrder.WebApi.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AxisOrder.WebApi.Controllers
{
    /// <summary>
    /// 统计控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors(ConstDefine.DefaultCrosPolicy)]
    public class StatisticsController : AuthorizedController
    {
        /// <summary>
        /// 首页统计信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("HomeCount")]
        public async Task<IActionResult> HomeCount()
        {
            return await Task.FromResult(Json(new { UserCount = Syllab.RandomProvider.RandomInteger(100, 1000) }));
        }
    }
}