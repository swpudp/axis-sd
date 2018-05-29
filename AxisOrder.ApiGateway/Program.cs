using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace AxisOrder.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ////构建自宿主
            //var host = new WebHostBuilder()
            //    .UseKestrel(o => o.AddServerHeader = false)
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .UseApplicationInsights()
            //    .CaptureStartupErrors(true)
            //    .Build();
            ////运行
            //host.Run();
            BuildWebHost().Run();
        }

        public static IWebHost BuildWebHost()
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                         .AddJsonFile("appsettings.json", true, true)
                         .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                         .AddJsonFile("configuration.json")
                         .AddEnvironmentVariables();
                 })
                 .ConfigureServices(s =>
                 {
                     s.AddOcelot();//.AddStoreOcelotConfigurationInConsul();
                 })
                 .ConfigureLogging(l =>
                 {
                     l.AddConsole();
                 })
                 .UseIISIntegration()
                 .Configure(app =>
                 {
                     app.UseOcelot().Wait();
                 })
                 .CaptureStartupErrors(true)
                .Build();

            //return WebHost.CreateDefaultBuilder(args)
            //    .ConfigureAppConfiguration((hostingContext, builder) =>
            //    {
            //        builder.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            //        .AddJsonFile("configuration.json")
            //        .AddJsonFile("hosting.json");
            //    })
            //    //.UseUrls("http://+:9000")
            //    .UseStartup<Startup>()
            //    .Build();
        }
    }
}
