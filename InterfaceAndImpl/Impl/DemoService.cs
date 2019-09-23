using InterfaceAndImpl.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceAndImpl.Impl {
    /// <summary>
    /// Impl
    /// </summary>
    public class DemoService : IDemoService {
        /// <summary>
        /// 打印信息
        /// </summary>
        public string SayHello() {
            return "Words 'Hello' Said By InterfaceAndImpl.Impl.DemoService";
        }
    }
}
