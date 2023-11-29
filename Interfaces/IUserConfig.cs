
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Dtos.Admin.Auth;
using XT.Common.Dtos.Admin.Menu;
using XT.Common.Models.Nav;
using XT.Common.Themes;

namespace XT.Common.Interfaces
{
    public interface IUserConfig
    {

        event EventHandler<bool> ChangeThemeEvent;


        /// <summary>
        /// 登录的用户
        /// </summary>
        public LoginUserOutput LoginUser { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public List<MenuOutput> Menus { get; set; }


        public List<NavItem> Navs { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public AppTheme Themes { get; set; }



        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public Task InitAllAsync();
        /// <summary>
        /// 设置主题
        /// </summary>
        public Task SetDarkOrLightTheme();

        /// <summary>
        /// 退出
        /// </summary>
        void Exit();

        /// <summary>
        /// 是否含有权限点
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool IsHasButtonWithRole(string code);
    }
}
