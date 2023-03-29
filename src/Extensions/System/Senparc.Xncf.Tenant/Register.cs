﻿using Senparc.Ncf.Core.Enums;
using Senparc.Ncf.XncfBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Senparc.Ncf.Core.Config;
using Senparc.CO2NET.Trace;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Senparc.Xncf.Tenant.Domain.DatabaseModel;
using Senparc.Ncf.XncfBase.Database;
using Senparc.Ncf.Core;
using Senparc.Ncf.Repository;
using Senparc.Ncf.Core.Models.DataBaseModel;
using Microsoft.AspNetCore.Builder;
using Senparc.CO2NET.RegisterServices;
using Microsoft.Extensions.Hosting;
using Senparc.Xncf.Tenant.OHS.Remote;
using Senparc.Xncf.Tenant.ACL.Repository;
using Senparc.Xncf.Tenant.Domain.DataBaseModel;
using Senparc.Xncf.Tenant.Domain.Models;
using Senparc.Ncf.Core.MultiTenant;
using AutoMapper;
using Senparc.Xncf.Tenant.Domain.DatabaseModel.AutoMapper;
using Senparc.Ncf.Database;

namespace Senparc.Xncf.Tenant
{
    [XncfRegister]
    [XncfOrder(5990)]
    public partial class Register : XncfRegisterBase, IXncfRegister
    {
        #region IXncfRegister 接口

        public override string Name => "Senparc.Xncf.Tenant";

        public override string Uid => SiteConfig.SYSTEM_XNCF_TANENT_UID;// "00000000-0000-0000-0000-000000000006";

        public override string Version => "0.1";//必须填写版本号

        public override string MenuName => "多租户";

        public override string Icon => "fa fa-university";//fa fa-cog

        public override string Description => "多租户模块，这是系统服务核心模块，请勿删除此模块。如果你实在忍不住，请务必做好数据备份。";

        public override async Task InstallOrUpdateAsync(IServiceProvider serviceProvider, InstallOrUpdate installOrUpdate)
        {
            //安装或升级数据库
            await XncfDatabaseDbContext.MigrateOnInstallAsync(serviceProvider, this);

            await base.InstallOrUpdateAsync(serviceProvider, installOrUpdate);
        }

        public override async Task UninstallAsync(IServiceProvider serviceProvider, Func<Task> unsinstallFunc)
        {
            //TODO：应该提供一个 BeforeUninstall 方法，阻止卸载。

            await base.UninstallAsync(serviceProvider, unsinstallFunc);
        }
        #endregion

        public override IServiceCollection AddXncfModule(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            AddMultiTenant(services);//注册多租户（按需）
            EntitySetKeys.TryLoadSetInfo(typeof(SenparcEntitiesMultiTenant));//注册多租户数据库的对象（按需）
            //EntitySetKeys.TryLoadSetInfo(typeof(TenantSenparcEntities));//注册多租户数据库的对象（按需）
            services.AddScoped<ITenantInfoDbData, TenantInfoDbData>();
            services.AddScoped<TenantInfoRepository>();
            services.AddScoped<IClientRepositoryBase<TenantInfo>, TenantInfoRepository>();

            //引入当前系统
            services.AddAutoMapper(z => z.AddProfile<TenantInfoProfile>());

            return base.AddXncfModule(services, configuration, env);
        }

        public override IApplicationBuilder UseXncfModule(IApplicationBuilder app, IRegisterService registerService)
        {
            #region 多租户

            app.UseMiddleware<TenantMiddleware>();//如果不启用多租户功能，可以删除此配置

            #endregion
            return base.UseXncfModule(app, registerService);
        }

        #region 扩展

        //public async Task<(bool success, string msg)> GenerateCreateScript(IServiceProvider serviceProvider)
        //{
        //    var success = true;
        //    string msg = null;

        //    //XncfModuleServiceExtension xncfModuleServiceExtension = serviceProvider.GetService<XncfModuleServiceExtension>();
        //    //SenparcEntities senparcEntities = (SenparcEntities)xncfModuleServiceExtension.BaseData.BaseDB.BaseDataContext;

        //    BasePoolEntities basePoolEntities = serviceProvider.GetService<BasePoolEntities>();

        //    try
        //    {
        //        SiteConfig.IsInstalling = true;

        //        //更新数据库
        //        var pendingMigs = await basePoolEntities.Database.GetPendingMigrationsAsync();
        //        if (pendingMigs.Count() > 0)
        //        {
        //            basePoolEntities.ResetMigrate();//重置合并状态

        //            try
        //            {
        //                var script = basePoolEntities.Database.GenerateCreateScript();
        //                SenparcTrace.SendCustomLog("senparcEntities.Database.GenerateCreateScript", script);

        //                basePoolEntities.Migrate();//进行合并

        //                msg = "已成功合并";
        //            }
        //            catch (Exception ex)
        //            {
        //                success = false;
        //                msg = ex.Message + "\r\n" + ex.StackTrace;
        //                var currentDatabaseConfiguration = DatabaseConfigurationFactory.Instance.Current;
        //                SenparcTrace.BaseExceptionLog(new NcfDatabaseException(ex.Message, currentDatabaseConfiguration.GetType(), basePoolEntities.GetType(), ex));
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        SiteConfig.IsInstalling = false;
        //    }

        //    return (success, msg);
        //}

        //public async Task<(bool success, string msg)> InitDatabase(IServiceProvider serviceProvider/*, TenantInfoService tenantInfoService*/
        //    /*HttpContext httpContext,*/)
        //{
        //    var success = false;
        //    string msg = null;

        //    //SenparcEntities senparcEntities = (SenparcEntities)xncfModuleServiceExtension.BaseData.BaseDB.BaseDataContext;
        //    using (var scope = serviceProvider.CreateScope())
        //    {
        //        var oldMultiTenant = SiteConfig.SenparcCoreSetting.EnableMultiTenant;
        //        //暂时关闭多租户状态
        //        SiteConfig.SenparcCoreSetting.EnableMultiTenant = false;

        //        var result = await GenerateCreateScript(serviceProvider);//尝试执行更新
        //        success = result.success;
        //        msg = result.msg;

        //        SiteConfig.SenparcCoreSetting.EnableMultiTenant = oldMultiTenant;
        //    }

        //    return (success: success, msg: msg);
        //}

        #endregion


        /// <summary>
        /// 添加多租户
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection AddMultiTenant(IServiceCollection services)
        {
            services.AddScoped<TenantInfoDto>();
            services.AddScoped<TenantInfoDbData>();

            //这个配置面相基类，不属于任何模块
            Func<IServiceProvider, SenparcEntitiesMultiTenant> multiTenantImplementationFactory = s =>
            {
                var multipleDatabasePool = MultipleDatabasePool.Instance;
                return multipleDatabasePool.GetDbContext<SenparcEntitiesMultiTenant>(serviceProvider: s);
            };
            services.AddScoped<SenparcEntitiesMultiTenant>(multiTenantImplementationFactory);//继承自 SenparcEntitiesMultiTenantBase

            return services;
        }


    }
}
