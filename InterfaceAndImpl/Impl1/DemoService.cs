using InterfaceAndImpl.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceAndImpl.Impl1 {
    /// <summary>
    /// Impl 1
    /// </summary>
    public class DemoService : IDemoService {
        /// <summary>
        /// 打印信息
        /// </summary>
        public string SayHello() {
            return "Words 'Hello' Said By InterfaceAndImpl.Impl1.DemoService";

        }
    }
}
