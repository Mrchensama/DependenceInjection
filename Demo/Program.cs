﻿using Autofac;
using InterfaceAndImpl.Interface;
using System;

namespace Demo {
    class Program {
        //程序入口
        static void Main(string[] args) {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<InterfaceAndImpl.Impl.DemoService>().As<IDemoService>().PropertiesAutowired().InstancePerLifetimeScope();

            //属性注入
            builder.Register<SetterInjection>(c => new SetterInjection() { _demoService = c.Resolve<IDemoService>() });
            builder.Register<SetterInjection>(c => new SetterInjection() { _demoService = c.Resolve<IDemoService>() });

            //需要属性注入的类型，注册时设置作为属性为自动注入
            builder.RegisterType<IDemoService>().PropertiesAutowired();

            builder.RegisterType<ConstructorInjection>();
            var container = builder.Build();
            SetterInjection setterInjection = container.Resolve<SetterInjection>();
            setterInjection.SayHello();
            ConstructorInjection constructorInjection = container.Resolve<ConstructorInjection>();
            constructorInjection.SayHello();

        }
    }
    #region 属性注入
    internal class SetterInjection {

        public IDemoService _demoService { set; get; }

        public void SayHello() {
            Console.WriteLine(_demoService.SayHello());
        }
    }
    #endregion

    #region 构造注入
    internal class ConstructorInjection {
        public readonly IDemoService _demoService;
        public ConstructorInjection(IDemoService demoService) {
            _demoService = demoService;
        }
        public void SayHello() {
            Console.WriteLine(_demoService.SayHello());
        }
    }
    #endregion

    #region 接口注入

    #endregion

    
}
