using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.DataAccess.Comm;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace WebApi
{
    public class Startup
    {
        [Obsolete]
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(env.ContentRootPath, "Config"))
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//增加环境配置文件，新建项目默认有
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                //将application层的注释添加的swaggerui中
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                var CommentsFileName = @"WebApi.xml";
                var CommentsFile = Path.Combine(baseDirectory, CommentsFileName);
                //将注释的Xml文档添加到swaggerUi中
                c.IncludeXmlComments(CommentsFile);
            });

            ConfigDatabase(services);
            services.AddMvc();
            //添加cors 服务 配置跨域处理            
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("any", builder =>
            //    {
            //        builder.AllowAnyOrigin() //允许任何来源的主机访问
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials();//指定处理cookie
            //    });
            //});

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //跨域配置
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
                //builder.WithOrigins("*");
            });
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

        }


        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="services"></param>
        public void ConfigDatabase(IServiceCollection services)
        {
            //var DataBaseSetting = Configuration.GetSection("DataBase");
            var sqlConnection = Configuration.GetConnectionString("SqlServerConnection");
            //var comDbConect = Configuration["DataBase:DbConnections"];
            services.AddDbContext<APIDBContext>(option =>
                option.UseSqlServer(sqlConnection, b => b.MigrationsAssembly("WebApi")));
            //services.Configure<AppSettings>
            //DataBase.GetConnectionString
        }


        public static ILoggerRepository repository { get; set; }
        /// <summary>
        /// 配置日志信息
        /// </summary>
        public void ConfigLog()
        {
            repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("Config/log4net.config"));
            //SDKProperties.LogRepository = repository;
        }

    }
}
