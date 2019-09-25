using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using InterfaceAndImpl.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace App.Controllers
{
    public class HomeController : Controller
    {
        #region 属性注入
        private IDemoService _demoService1 { set; get; }
        #endregion

        #region 构造注入
        public readonly IDemoService _demoService2;
        public HomeController(IDemoService demoService)
        {
            _demoService2 = demoService;
        }
        #endregion

        public IActionResult Index()
        {
            IndexPageModel model = new IndexPageModel();
            model.Text1 = _demoService1.SayHello();
            model.Text2 = _demoService2.SayHello();
            return View(model);
        }

        #region IServiceProvider.GetService
        //情景模拟：当该类中只有以下方法需要使用IDemoService的实例时
        //          可以考虑使用IServiceProvider.GetService来获取
        public void SayHello()
        {
            Console.WriteLine(Startup.ServiceProvider.GetService<IDemoService>().SayHello());
        }
        #endregion

        #region 服务定位器（ServiceLocator）
        public void SayHello()
        {
            Console.WriteLine(Startup.Container.Get<IDemoService>().SayHello());
        }
        #endregion
    }
}