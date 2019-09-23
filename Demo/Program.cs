using Autofac;
using InterfaceAndImpl.Interface;
using System;

namespace Demo {
    class Program {
        //程序入口
        static void Main(string[] args) {
            //var injection = new InjectionClass();
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<InterfaceAndImpl.Impl.DemoService>().As<IDemoService>().PropertiesAutowired().InstancePerLifetimeScope();
            builder.Register<InjectionClass>(c => new InjectionClass() { _demoService1 = c.Resolve<IDemoService>() });
            var container = builder.Build();
            InjectionClass injectionClass = container.Resolve<InjectionClass>();
            injectionClass.SayHello1();
            //injectionClass.SayHello2();
        }
    }
    internal class InjectionClass {
        #region 属性注入
        public IDemoService _demoService1 { set; get; }
        #endregion
        #region 构造注入
        public readonly IDemoService _demoService2;
        //public InjectionClass(IDemoService demoService) {
        //    _demoService2 = demoService;
        //}
        #endregion

        #region 接口注入

        #endregion
        public void SayHello1() {
            Console.WriteLine(_demoService1.SayHello());
        }
        //public void SayHello2() {
        //    Console.WriteLine(_demoService2.SayHello());
        //}
    }

}
