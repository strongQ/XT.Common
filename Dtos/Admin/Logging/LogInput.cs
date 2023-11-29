
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Util;

namespace XT.Common.Dtos.Admin.Logging
{
    public class PageLogInput : BasePageInput
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }



    public class AddLogInput : BaseIdInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        [HeadDescription("账号")]
        public string Account { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [HeadDescription("模块名称")]
        public string ControllerName { get; set; }

        /// <summary>
        /// 方法名称
        ///</summary>
        [HeadDescription("方法名称")]
        public string ActionName { get; set; }

        /// <summary>
        /// 显示名称
        ///</summary>
        [HeadDescription("显示名称")]
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [Description("请求方式")]
        public string HttpMethod { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [HeadDescription("请求地址")]
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        [Description("请求参数")]
        public string RequestParam { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        [Description("返回结果")]
        public string ReturnResult { get; set; }





        /// <summary>
        /// 异常信息
        /// </summary>
        [Description("异常信息")]
        public string Exception { get; set; }

        /// <summary>
        /// 日志消息Json
        /// </summary>
        [Description("日志消息")]
        public string Message { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        [HeadDescription("执行状态")]
        public string Status { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [HeadDescription("请求IP")]
        public string RemoteIp { get; set; }

        /// <summary>
        /// 登录地点
        /// </summary>
        [HeadDescription("登录地点")]
        public string Location { get; set; }



        /// <summary>
        /// 浏览器
        /// </summary>
        [Description("浏览器")]
        public string Browser { get; set; }



        /// <summary>
        /// 操作用时
        /// </summary>
        [HeadDescription("操作用时")]
        public long? Elapsed { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        [HeadDescription("日志时间")]
        public DateTime? LogDateTime { get; set; }





    }
}
