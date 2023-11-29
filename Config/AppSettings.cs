using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using XT.Common.Extensions;

namespace XT.Common.Config
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 全局配置
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 是否开发环境
        /// </summary>
        public static bool IsDevelopment { get; set; }


        public AppSettings(bool isDevelopment, string rootPath = "")
        {
            IsDevelopment = isDevelopment;

            //根据环境读取响应的appsettings
            string appsettingsFile = IsDevelopment ? "appsettings.Development.json" : "appsettings.json";

            if (Configuration != null) return;
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appsettingsFile);

            Configuration = builder.Build();
        }

        /// <summary>
        /// 获取对象值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObjData<T>(string key = "") where T : new()
        {
            T option = new T();
            if (key.IsNullOrEmpty())
            {
                key = typeof(T).Name;
            }
            Configuration.GetSection(key).Bind(option);
            return option;

        }

        /// <summary>
        /// 从AppSettings获取key的值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            try
            {
                return Configuration[key];
            }
            catch
            {
                // ignored
            }

            return "";
        }

        /// <summary>
        /// 从AppSettings获取key的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键路径</param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
        {
            return ConvertValue<T>(GetValue(key));
        }


        /// <summary>
        /// 从AppSettings获取key的值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GetValue(params string[] keys)
        {
            try
            {
                if (keys.Any())
                {
                    return Configuration[string.Join(":", keys)];
                }
            }
            catch
            {
                // ignored
            }

            return "";
        }

        /// <summary>
        /// 从AppSettings获取key的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="keys">键路径</param>
        /// <returns></returns>
        public static T GetValue<T>(params string[] keys)
        {
            return ConvertValue<T>(GetValue(keys));
        }


        /// <summary>
        /// 值类型转换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static T ConvertValue<T>(string value)
        {
            return (T)ConvertValue(typeof(T), value);
        }

        /// <summary>
        /// 值类型转换
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static object ConvertValue(Type type, string value)
        {
            if (type == typeof(object))
            {
                return value;
            }

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return string.IsNullOrEmpty(value) ? value : ConvertValue(Nullable.GetUnderlyingType(type), value);
            }

            var converter = TypeDescriptor.GetConverter(type);
            return converter.CanConvertFrom(typeof(string)) ? converter.ConvertFromInvariantString(value) : null;
        }
    }
}
