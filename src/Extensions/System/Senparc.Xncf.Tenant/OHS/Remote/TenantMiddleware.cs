﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.Trace;
using Senparc.Ncf.Core;
using Senparc.Ncf.Core.Config;
using Senparc.Xncf.Tenant.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Senparc.Xncf.Tenant.OHS.Remote
{
    /// <summary>
    /// 多租户中间件
    /// </summary>
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //判断是否需要使用数据库
                if (!SiteConfig.DatabaseXncfLoaded)
                {
                    //没有数据库，跳过
                    await _next(context);
                }

                var serviceProvider = context.RequestServices;
                var tenantInfoService = serviceProvider.GetRequiredService<TenantInfoService>();
                var requestTenantInfo = await tenantInfoService.SetScopedRequestTenantInfoAsync(context);//设置当前 Request 的 RequestTenantInfo 参数
            }
            catch (NcfUninstallException unInstallEx)
            {
                Console.WriteLine("\t NcfUninstallException from TenantMiddleware");
                context.Items["NcfUninstallException"] = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\t Exception from TenantMiddleware");

                //如果数据库出错
                SenparcTrace.BaseExceptionLog(ex);
                throw;
            }
            //Console.WriteLine($"\tTenantMiddleware requestTenantInfo({requestTenantInfo.GetHashCode()})：" + requestTenantInfo.ToJson());

            await _next(context);
            //Console.WriteLine("TenantMiddleware finished");
        }
    }
}
