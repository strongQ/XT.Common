using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XT.Common.Interfaces;
using XT.Common.Services;

namespace XT.Common.Helpers
{
    /// <summary>
    /// 导出服务
    /// </summary>
    public class ProviderHelper
    {
        public static IProviderService ProviderService;
        /// <summary>
        /// 根据视图名称获取VM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vmName"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public static IModuleViewModel GetViewModels(string viewName, string space)
        {
            var vms = ProviderService.GetServices<IModuleViewModel>();

            var vm = vms?.FirstOrDefault(x => x.GetType().Name.Replace("VM", "").Equals(viewName) && x.GetType().Namespace.Contains(space.Replace(".Views", "")));

            return vm;
        }
    }
}
