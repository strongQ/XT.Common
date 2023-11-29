using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Interfaces
{
    public interface IModuleStart
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="dllName"></param>
        void AddServices(ServiceCollection services, IConfigurationRoot configuration, string dllName);
    }
}
