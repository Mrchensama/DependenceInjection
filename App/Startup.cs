using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.AppStart;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using InterfaceAndImpl.Impl;
using InterfaceAndImpl.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App {
    public class Startup {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        /// <summary>
        /// ServiceProvider
        /// </summary>
        public static IServiceProvider ServiceProvider { private set; get; }
        /// <summary>
        /// Container
        /// </summary>
        public static IContainer Container { private set; get; }
        /// <summary>
        /// 服务代理设置
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            //每次GetService都会获得一个新的实例
            services.AddTransient<IDemoService, DemoService>();

            //在同一个Scope内只初始化一个实例 ，可以理解为（ 每一个request级别只创建一个实例，同一个http request会在一个 scope内）
            services.AddScoped<IDemoService, DemoService>();

            //整个应用程序生命周期以内只创建一个实例 
            services.AddSingleton<IDemoService, DemoService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddControllersAsServices();
            ContainerBuilder builder = new ContainerBuilder();
            //新模块组件注册
            builder.RegisterModule<DefaultModule>();
            //将services中的服务填充到Autofac中.
            builder.Populate(services);
            //创建容器.
            Container = builder.Build();
            //使用容器创建 AutofacServiceProvider 
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }
            //配置路由
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
