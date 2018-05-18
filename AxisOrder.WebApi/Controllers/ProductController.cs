using AxisOrder.Models.Params;
using AxisOrder.Models.Views;
using AxisOrder.WebApi.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Syllab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AxisOrder.WebApi.Controllers
{
    /// <summary>
    /// 产品管理控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors(ConstDefine.DefaultCrosPolicy)]
    public class ProductController : AuthorizedController
    {
        private static IList<ProductView> _products = Enumerable.Range(0, 1000).Select((x, idx) => new ProductView
        {
            Id = NewId.StringId(),
            Name = RandomProvider.RandomString(4),
            Code = RandomProvider.RandomString(6),
            Price = RandomProvider.RandomInteger(1800, 19000),
            Version = idx
        }).ToList();


        /// <summary>
        /// 指定查询条件查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody]ProductParam param)
        {
            Func<ProductView, bool> where = e => true;
            if (!string.IsNullOrEmpty(param.Name))
            {
                where += p => p.Name.Contains(param.Name);
            }
            if (!string.IsNullOrEmpty(param.Code))
            {
                where += p => p.Code.Contains(param.Code);
            }
            var result = _products.Where(where).Skip((param.Current - 1) * param.PageSize).Take(param.PageSize).ToList();
            return await Task.FromResult(Json(new { total = _products.Count(where), rows = result }));
        }
    }
}