using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Extensions;

namespace XT.Common.Attributes
{
    /// <summary>
    /// Page 页面特性，自动生成页面菜单
    /// </summary>
    public class PageAttribute : Attribute
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 单页面显示
        /// </summary>
        public bool Show { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Page 页面特性，自动生成页面菜单 
        /// 用法：Page("/ecs/floor1", "一楼", "mdi-home-floor-1", true)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">名称</param>
        /// <param name="icon">图标</param>
        /// <param name="show">是否显示</param>
        public PageAttribute(string path, string name, string icon = "", bool show = false)
        {
            Path = path;
            Name = name;
            Show = show;
            if (icon.IsNullOrEmpty())
            {
                Icon = "mdi-home";
            }
            else
            {
                Icon = icon;
            }
        }


    }
    /// <summary>
    /// 页面
    /// </summary>
    public class RazorPageModel
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public bool Show { get; set; }
        public string Icon { get; set; }
    }
}
