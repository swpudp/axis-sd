using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AxisOrder.WebApi
{
    /// <summary>
    /// Api入口程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //使用hosting.json指定运行端等
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json")
                .Build();

            //构建自宿主
            WebHost.CreateDefaultBuilder<Startup>(args)
                .UseKestrel(o => o.AddServerHeader = false)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseConfiguration(config)
                //.UseStartup<Startup>()
                //.UseApplicationInsights()
                .CaptureStartupErrors(true)
                .Build()
                .Run();//运行
        }
    }
}
