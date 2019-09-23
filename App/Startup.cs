﻿using System;
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
        /// 服务代理设置
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddControllersAsServices();
            ContainerBuilder builder = new ContainerBuilder();
            //新模块组件注册
            builder.RegisterModule<DefaultModule>();
            //将services中的服务填充到Autofac中.
            builder.Populate(services);
            //创建容器.
            var autofacContainer = builder.Build();
            //使用容器创建 AutofacServiceProvider 
            return new AutofacServiceProvider(autofacContainer);
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