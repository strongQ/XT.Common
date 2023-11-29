
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XT.Common.Interfaces;
using XT.Common.Models.Server;

namespace XT.Common.Extensions
{

    public static class ActionExtension
    {
        public static string ProtectAction(this Action action)
        {

            try
            {
                action();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        public static async Task<string> ProtectAction(this Func<Task> action)
        {

            try
            {
                await action();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        public static async Task<(T, string)> ProtectFunc<T>(this Func<Task<T>> func)
        {

            try
            {
                var data = await func();
                return (data, "");
            }
            catch (Exception ex)
            {
                return (default(T), ex.Message);
            }

        }

        public static (T, string) ProtectFunc<T>(this Func<T> func)
        {

            try
            {
                var data = func();
                return (data, "");
            }
            catch (Exception ex)
            {
                return (default(T), ex.Message);
            }

        }


        /// <summary>
        /// 获取admin Get数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="userConfig"></param>
        /// <returns></returns>
        public static async Task<AdminCodeResult<T>> GetAdminData<T>(this HttpClient httpClient, string url, IApiConfig userConfig, object data = null)
        {
            try
            {
                string query = data?.ToQueryString();


                var result = await httpClient.GetAsync($"{url}?{query}");



                return await result.AdminResult<T>(userConfig);
            }
            catch (Exception ex)
            {
                return new AdminCodeResult<T>
                {
                    Message = ex.Message
                };
            }
        }
        /// <summary>
        /// 获取Http数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<(T, string)> GetHttpData<T>(this HttpClient httpClient, string url, object data = null)
        {
            try
            {
                string query = data?.ToQueryString();


                var result = await httpClient.GetAsync($"{url}?{query}");

                if (result.IsSuccessStatusCode)
                {
                    var http = await result.Content.ReadFromJsonAsync<T>();
                    return (http, string.Empty);
                }

                return (default, string.Empty);
            }
            catch (Exception ex)
            {
                return (default, ex.Message);
            }
        }

        /// <summary>
        /// 获取Http数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<(string, string)> GetHttpData(this HttpClient httpClient, string url, object data = null)
        {
            try
            {
                string query = data?.ToQueryString();


                var result = await httpClient.GetAsync($"{url}?{query}");

                if (result.IsSuccessStatusCode)
                {
                    var http = await result.Content.ReadAsStringAsync();
                    return (http, string.Empty);
                }

                return (default, string.Empty);
            }
            catch (Exception ex)
            {
                return (default, ex.Message);
            }
        }

        /// <summary>
        /// post提交数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<(T, string)> PostHttpData<T>(this HttpClient httpClient, string url, object data)
        {
            try
            {



                var result = await httpClient.PostAsJsonAsync(url, data);

                if (result.IsSuccessStatusCode)
                {
                    var obj = await result.Content.ReadFromJsonAsync<T>();

                    return (obj, string.Empty);
                }


                return (default, ((int)result.StatusCode).ToString());
            }
            catch (Exception ex)
            {
                return (default, ex.Message);
            }
        }

        /// <summary>
        /// 获取admin Post数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="userConfig"></param>
        /// <returns></returns>
        public static async Task<AdminCodeResult<T>> PostAdminData<T>(this HttpClient httpClient, string url, IApiConfig userConfig, object data = null)
        {
            try
            {



                var result = await httpClient.PostAsJsonAsync(url, data);

                return await result.AdminResult<T>(userConfig);
            }
            catch (Exception ex)
            {
                return new AdminCodeResult<T>
                {
                    Message = ex.Message
                };
            }
        }
        /// <summary>
        /// 提交返回Admin
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="userConfig"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<AdminCodeResult<T>> PostAdmin<T>(this HttpClient httpClient, string url, IApiConfig userConfig, HttpContent data = null)
        {
            try
            {



                var result = await httpClient.PostAsync(url, data);

                return await result.AdminResult<T>(userConfig);
            }
            catch (Exception ex)
            {
                return new AdminCodeResult<T>
                {
                    Message = ex.Message
                };
            }
        }
        /// <summary>
        /// result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="userConfig"></param>
        /// <returns></returns>
        public static async Task<AdminCodeResult<T>> AdminResult<T>(this HttpResponseMessage response, IApiConfig userConfig)
        {
            if (!response.IsSuccessStatusCode)
            {
                return new AdminCodeResult<T>
                {
                    Code = (int)response.StatusCode
                };
            }
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<AdminCodeResult<T>>(responseAsString);
            // 没有权限
            if (responseObject.Code == 401 || responseObject.Code == 403)
            {
                userConfig.AuthorizationFailedInvoke();
            }
            return responseObject;
        }

        /// <summary>
        /// 注入接口服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceInjects<T>(this IServiceCollection services, Type managers = null, bool isSingleton = false) where T : class
        {
            if (managers == null)
            {
                managers = typeof(T);
            }
            var allTypes = managers.Assembly.GetExportedTypes();

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (typeof(T).IsAssignableFrom(type.Service))
                {
                    if (isSingleton)
                    {
                        services.AddSingleton(type.Service, type.Implementation);
                    }
                    else
                    {
                        services.AddScoped(type.Service, type.Implementation);
                    }

                }
            }

            return services;
        }

    }
}
