using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using App.Controllers;
using Autofac;
using Autofac.Core;
using InterfaceAndImpl.Impl;
using InterfaceAndImpl.Interface;
using Microsoft.AspNetCore.Mvc;

namespace App.AppStart
{
    public class DefaultModule : Autofac.Module
    {
        public static DemoService Instance = new DemoService();
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InterfaceAndImpl.Impl.DemoService>().As<IDemoService>().PropertiesAutowired().InstancePerLifetimeScope();
            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();

            builder.Register(x => new DemoService()).As<IDemoService>();
            builder.Register(x => new InterfaceAndImpl.Impl1.DemoService(x.Resolve<IList<string>>())).As<IDemoService>();

            builder.RegisterGeneric(typeof(List<>))
                .As(typeof(IList<>)).InstancePerLifetimeScope();


            builder.RegisterType<DemoService>().AsSelf()
                .IfNotRegistered(typeof(IDemoService));

            builder.RegisterType<DemoService>().As<IDemoService>()
                .OnlyIf(x =>
            x.IsRegistered(new TypedService(typeof(IDemoService))));


            Assembly assembly = Assembly.Load("App.InterfaceAndImpl");

            builder.RegisterAssemblyTypes(assembly)//程序集内所有具象类（concrete classes）
                .Where(cc => cc.Name.EndsWith("Service"))
                .PublicOnly()//只要public访问权限的
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                .Except<InterfaceAndImpl.Impl1.DemoService>()//InterfaceAndImpl.Impl1.DemoService
                .As(x=>x.GetInterfaces()[0])//反射出其实现的接口，默认以第一个接口类型暴露
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IBaseRepository<>));


            //不指定，默认就是瞬时的
            builder.RegisterType<DemoService>();
            //指定其生命周期域为瞬时
            builder.RegisterType<DemoService>().InstancePerDependency();

            //指定其生命周期域为全局单例
            builder.RegisterType<DemoService>().SingleInstance();

            //指定其生命周期域为域内单例
            builder.RegisterType<DemoService>().InstancePerLifetimeScope();

            builder.RegisterType<DemoService>().InstancePerMatchingLifetimeScope("myTag");
        }
    }
}
