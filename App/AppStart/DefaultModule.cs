using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Controllers;
using Autofac;
using InterfaceAndImpl.Interface;
using Microsoft.AspNetCore.Mvc;

namespace App.AppStart {
    public class DefaultModule : Autofac.Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<InterfaceAndImpl.Impl.DemoService>().As<IDemoService>().PropertiesAutowired().InstancePerLifetimeScope();
            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();
        }
    }
}
