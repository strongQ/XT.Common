using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Interfaces
{
    public interface IModulePage
    {
        public string PageName { get; set; }

        public string Icon { get; set; }
        /// <summary>
        /// 标志
        /// </summary>
        public string PageTag { get; set; }


    }
}
