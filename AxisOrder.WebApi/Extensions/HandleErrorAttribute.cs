using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AxisOrder.WebApi.Extensions
{
    /// <summary>
    /// 异常处理过滤器
    /// </summary>
    public class HandleErrorAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="factory"></param>
        public HandleErrorAttribute(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger(GetType());
        }

        /// <summary>
        /// 当请求发生错误时执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            context.ExceptionHandled = true;
            context.Result = new JsonResult(new { IsSucceed = false, context.Exception.Message });
        }
    }
}
