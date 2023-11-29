using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Services
{
    public interface IConfigService
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetConfig(string key);
    }
}
