using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Services
{
    public interface IProviderService
    {
        /// <summary>
        /// 获取ioc容器中的服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetService<T>() where T : class;
        /// <summary>
        /// 获取ioc容器中的服务集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetServices<T>() where T : class;
    }
}
