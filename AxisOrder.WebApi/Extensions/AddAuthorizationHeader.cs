using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace AxisOrder.WebApi.Extensions
{
    /// <summary>
    /// 自定义请求头
    /// </summary>
    public class AddAuthorizationHeader : IOperationFilter
    {
        /// <summary>
        /// 应用过滤器
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null || !operation.Parameters.Any())
            {
                operation.Parameters = new List<IParameter>();
            }
            //获取api控制器Attribute
            var attributes = context.ApiDescription.ControllerAttributes();
            foreach (var attribute in attributes)
            {
                //如果是AuthorizeAttribute
                if (attribute.GetType() == typeof(AuthorizeAttribute))
                {
                    //添加Authorization请求头
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Name = "Authorization",//header名称
                        In = "header",//在header添加
                        Type = "string",//类型
                        Required = true,//是否必须
                        Description = "Token认证参数"//描述
                    });
                }
            }
        }
    }
}
