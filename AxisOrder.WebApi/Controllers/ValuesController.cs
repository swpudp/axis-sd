using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AxisOrder.Models.Commands;
using AxisOrder.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Syllab;
using Syllab.Driver.Commanding;

namespace AxisOrder.WebApi.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// 命令总线
        /// </summary>
        private readonly ICommandBus _bus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bus"></param>
        public ValuesController(ICommandBus bus)
        {
            _bus = bus;
        }


        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// PUT api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// 执行命令测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("Exec")]
        public async Task<IActionResult> ExecTest()
        {
            var id = NewId.StringId();
            var cmd = new UserRegisterCommand(id, new UserRegisterEntity
            {

                Id = id,
                FullName = NewId.StringId(),
                IdNo = NewId.StringId(),
                LoginName = NewId.StringId(),
                Mobile = NewId.StringId(),
                Password = NewId.StringId(),
                Version = 1
            });
            var result = await _bus.ExecuteAsync(cmd);
            return Json(result);
        }
    }
}
