using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XT.Common.Handlers;
using XT.Common.Interfaces;
using XT.Common.SignalR;
using Microsoft.Extensions.Http;

namespace XT.Common
{
    public static class Register
    {
        /// <summary>
        /// signalR实时应用  connect
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddSignalRService(this IServiceCollection services)
        {
            return services.AddSingleton<ISignalRService, SignalRService>();
        }


        ///// <summary>
        ///// Excel导入导出
        ///// </summary>
        ///// <param name="services"></param>
        //public static IServiceCollection AddExcelService(this IServiceCollection services)
        //{
        //    return services.AddSingleton<IExcelService, ExcelService>();
        //}




        /// <summary>
        /// 添加httpclient
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOriginHttpClient(this IServiceCollection services)
        {
            services.AddTransient<AuthHeaderHandler>();
            services.ConfigureHttpClientDefaults(builder =>
            {
                builder.AddHttpMessageHandler<AuthHeaderHandler>();
                // 您也可以在这里设置一些全局默认值，如 Timeout
                builder.SetHandlerLifetime(TimeSpan.FromMinutes(5)); // IHttpClientFactory 建议的设置
                builder.ConfigureHttpClient(client => client.Timeout = TimeSpan.FromSeconds(30));
            });
            return services;
        }

      

        //public static IServiceCollection AddGrpcChannelService(this IServiceCollection services)
        //{
        //    return services.AddSingleton<IGrpcChannelService, GrpcChannelService>();
        //}
        //// AddGrpcClient
        //public static void AddGrpcClientService<T>(this IServiceCollection services, string url) where T : class
        //{
        //    //this registers ICalculator Grpc client service
        //    services.AddCodeFirstGrpcClient<T>(o =>
        //    {

        //        // Address of grpc server
        //        o.Address = new Uri(url);

        //        // another channel options (based on best practices docs on https://docs.microsoft.com/en-us/aspnet/core/grpc/performance?view=aspnetcore-6.0)
        //        o.ChannelOptionsActions.Add(options =>
        //        {
        //            //options.HttpHandler = new SocketsHttpHandler()
        //            //{
        //            //    // keeps connection alive
        //            //    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
        //            //    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
        //            //    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),

        //            //    // allows channel to add additional HTTP/2 connections
        //            //    EnableMultipleHttp2Connections = true
        //            //};
        //            options.MaxReceiveMessageSize = null;
        //            options.MaxSendMessageSize = null;
        //            options.MaxRetryAttempts = int.MaxValue;

        //            options.ServiceConfig = new ServiceConfig
        //            {
        //                MethodConfigs = {new MethodConfig
        //            {
        //                Names = { MethodName.Default },
        //                RetryPolicy = new RetryPolicy
        //                {
        //                    MaxAttempts = 3,
        //                    InitialBackoff = TimeSpan.FromSeconds(1),
        //                    MaxBackoff = TimeSpan.FromSeconds(5),
        //                    BackoffMultiplier = 1.5,
        //                    RetryableStatusCodes = { StatusCode.Unavailable }
        //                }
        //            }
        //        }
        //            };

        //        });
        //    });
        //}
    }
}
