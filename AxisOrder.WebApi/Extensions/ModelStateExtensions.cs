using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace AxisOrder.WebApi.Extensions
{
    /// <summary>
    /// 模型验证扩展函数
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 获取模型验证错误信息
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IList<string> GetErrors(this ModelStateDictionary modelState)
        {
            var group = from vals in modelState.Values
                        let msgs = vals.Errors
                        from mes in msgs
                        let infos = mes.ErrorMessage
                        select infos;
            return group.ToList();
        }
    }
}
