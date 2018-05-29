using AxisOrder.SoapMiddleware;
using Microsoft.AspNetCore.Builder;
using Syllab.Utils;
using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;

namespace AxisOrder.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SOAPMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblys"></param>
        /// <param name="binding"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSoapMiddleware(this IApplicationBuilder builder, Assembly[] assemblys, Binding binding, Action action = null)
        {
            if (assemblys == null || !assemblys.Any())
            {
                throw new Exception("请指定服务程序集");
            }
            action?.Invoke();
            foreach (var assembly in assemblys)
            {
                var types = assembly.GetTypes().Where(x => TypeUtils.IsComponent(x));
                foreach (var type in types)
                {
                    var encoder = binding.CreateBindingElements().Find<MessageEncodingBindingElement>()?.CreateMessageEncoderFactory().Encoder;
                    builder.UseMiddleware<SoapOfMiddleware>(type, $"/{type.Name}.svc", encoder);
                }
            }
            return builder;
        }
    }
}
