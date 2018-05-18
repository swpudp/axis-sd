using Autofac;
using AxisOrder.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Syllab.Configurations;
using Syllab.Driver;
using Syllab.Driver.InMemory;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace AxisOrder.WebApi
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="environment"></param>
        public Startup(IHostingEnvironment environment)
        {
            //1、加载配置文件
            var config = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("hosting.json", true, true)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true);

            //开发环境使用监控设置
            if (environment.IsDevelopment())
            {
                config.AddApplicationInsightsSettings();
            }

            //添加环境变量
            config.AddEnvironmentVariables();

            //构建配置
            ConfigManager.Configuration = config.Build();
        }

        /// <summary>
        /// 配置项
        /// </summary>
        private IConfiguration Configuration { get; set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //使用log4net日志
            services.AddLogging(c => c.AddProvider(new Log4NetProvider("log4net.config")));

            services.AddTransient(typeof(Lazy<>));

            //加载程序集注入依赖项
            var assemblies = new Assembly[] {
                Assembly.Load("AxisOrder.QueryContract"),
                Assembly.Load("AxisOrder.QueryImplement"),
                Assembly.Load("AxisOrder.ProcessManager")
            };

            //使用IOptions
            services.AddOptions();

            //注册基础框架组件
            services.AddCommon()
                .AddBusiness(assemblies)
                .AddDriver(s => s.UseMemory());

            //读取jwt配置项
            var jwtSection = ConfigManager.Configuration.GetSection(nameof(JwtOptions));
            services.Configure<JwtOptions>(jwtSection);

            //添加JwtBearer Token认证
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var options = jwtSection.Get<JwtOptions>();
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = options.Audience,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                    ValidateLifetime = options.ValidateLifetime,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true
                };
            });


            //启用跨域支持
            services.AddCors(o =>
            {
                var opt = ConfigManager.Configuration.GetSection(nameof(CorsOption));
                var urls = opt.Get<CorsOption>().Urls;
                o.AddPolicy(ConstDefine.DefaultCrosPolicy, b => b.WithOrigins(urls).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });

            //移除自带的控制器激活依赖注入组件
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //注册Mvc组件以及设置Json数据格式
            services.AddMvc().AddJsonOptions(o =>
            {
                o.SerializerSettings.ContractResolver = new DefaultContractResolver();
                o.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            //使用Swagger生成开发API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "订单系统",
                    Version = "v1",
                    Contact = new Contact { Email = "swpudp@163.com", Name = "丁品" },
                    Description = "为前端开发人员提供数据交换API"
                });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "AxisOrder.WebApi.xml");
                c.IncludeXmlComments(filePath);
                c.OperationFilter<AddAuthorizationHeader>();
            });

            //替换默认依赖注入组件，改用Autofac
            return services.BuildProvider(c =>
            {
                var controllerType = typeof(ControllerBase);
                var controllerAssemblys = new[] { Assembly.Load("AxisOrder.WebApi") };
                c.RegisterAssemblyTypes(controllerAssemblys)
                    .Where(assembly => controllerType.IsAssignableFrom(assembly) && assembly != controllerType)
                    .PropertiesAutowired();
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// 配置http请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="factory"></param>
        /// <param name="options"></param>
        /// 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory, IOptions<CorsOption> options)
        {
            //使用log4net作为日志记录工具
            factory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //重定向相关
            var header = new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto };
            app.UseForwardedHeaders(header);

            //使用权限认证
            app.UseAuthentication();

            //启用Mvc组件
            app.UseMvc();

            //启动框架处理程序
            app.UseDriver();

            //启用Swagger
            app.UseSwagger();

            //启用Swagger UI界面
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "订单系统Ver 1");
            });
        }
    }
}
