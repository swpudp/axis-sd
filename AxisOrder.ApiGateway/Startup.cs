//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using CacheManager.Core;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Ocelot.DependencyInjection;
//using Ocelot.Middleware;

//namespace AxisOrder.ApiGateway
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        //public Startup(IHostingEnvironment environment)
//        {
//            //var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
//            //builder.SetBasePath(Directory.GetCurrentDirectory())
//            //       .AddJsonFile("appsettings.json", false, true)
//            //       .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
//            //       .AddJsonFile("configuration.json", false, true)
//            //       .AddEnvironmentVariables();
//            //Configuration = builder.Build();
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddOcelot(Configuration);
//            //services.AddMvc();
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
//        {
//            app.UseOcelot().Wait();
//            //if (env.IsDevelopment())
//            //{
//            //    app.UseDeveloperExceptionPage();
//            //}

//            //app.UseMvc();
//        }
//    }
//}
