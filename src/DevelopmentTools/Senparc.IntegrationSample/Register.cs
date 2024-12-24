using Microsoft.Extensions.Options;

using Senparc.CO2NET;
using Senparc.CO2NET.AspNet;
using Senparc.CO2NET.Utilities;
using Senparc.CO2NET.WebApi.WebApiEngines;
using Senparc.Ncf.Core.Config;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.XncfBase;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson;
using Senparc.Xncf.AreasBase;

namespace Senparc.IntegrationSample
{
    /// <summary>
    /// 全局注册
    /// </summary>
    public static class Register
    {
        public static DateTime StartTime { get; }

        static Register()
        {
            StartTime = System.DateTime.Now;
        }

        public static void AddNcf(this WebApplicationBuilder builder)
        {
            //激活 Xncf 扩展引擎（必须）
            var logMsg = builder.StartWebEngine(new[] { "Senparc.IntegrationSample.AreaTests","Senparc.Weixin.MP" });
            Console.WriteLine("============ logMsg =============");
            Console.WriteLine(logMsg);
            Console.WriteLine("============ logMsg END =============");

            //var mvcCoreBuilder = builder.Services.AddMvcCore();
            //builder.Services.AddAndInitDynamicApi(mvcCoreBuilder, options => options.DocXmlPath = ServerUtility.ContentRootMapPath("~/App_Data/ApiDocXml"));
        }

        public static void UseNcf<TDatabaseConfiguration>(this WebApplication app)
            where TDatabaseConfiguration : IDatabaseConfiguration, new()
        {
            var env = app.Environment;

            // 启动 CO2NET 全局注册，必须！
            // 关于 UseSenparcGlobal() 的更多用法见 CO2NET Demo：https://github.com/Senparc/Senparc.CO2NET/blob/master/Sample/Senparc.CO2NET.Sample.netcore3/Startup.cs
            var registerService = app
                //全局注册
                .UseSenparcGlobal(env, null, globalRegister =>
                {
                    //配置全局使用Redis缓存（按需，独立）
                    if (UseRedis(null, out var redisConfigurationStr))//这里为了方便不同环境的开发者进行配置，做成了判断的方式，实际开发环境一般是确定的，这里的if条件可以忽略
                    {
                        /* 说明：
                         * 1、Redis 的连接字符串信息会从 Config.SenparcSetting.Cache_Redis_Configuration 自动获取并注册，如不需要修改，下方方法可以忽略
                        /* 2、如需手动修改，可以通过下方 SetConfigurationOption 方法手动设置 Redis 链接信息（仅修改配置，不立即启用）
                         */
                        CO2NET.Cache.CsRedis.Register.SetConfigurationOption(redisConfigurationStr);

                        //以下会立即将全局缓存设置为 Redis
                        CO2NET.Cache.CsRedis.Register.UseKeyValueRedisNow(); //键值对缓存策略（推荐）
                        /*Senparc.CO2NET.Cache.CsRedis.Register.UseHashRedisNow();*/ //HashSet储存格式的缓存策略 

                        //也可以通过以下方式自定义当前需要启用的缓存策略
                        //CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//键值对
                        //CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisHashSetObjectCacheStrategy.Instance);//HashSet
                    }
                    //如果这里不进行Redis缓存启用，则目前还是默认使用内存缓存 
                });

            //XncfModules（必须）
            app.UseXncfModules(registerService);

            //必须在 UseXncfModules 之后
            app.UseNcfDatabase(typeof(TDatabaseConfiguration));

        }

        /// <summary>
        /// 判断当前配置是否满足使用 Redis（根据是否已经修改了默认配置字符串判断）
        /// </summary>
        /// <param name="senparcSetting"></param>
        /// <returns></returns>
        internal static bool UseRedis(SenparcSetting? senparcSetting, out string redisConfigurationStr)
        {
            senparcSetting ??= Senparc.CO2NET.Config.SenparcSetting;
            redisConfigurationStr = senparcSetting.Cache_Redis_Configuration;
            var useRedis = !string.IsNullOrEmpty(redisConfigurationStr) && redisConfigurationStr != "#{Cache_Redis_Configuration}#"/*默认值，不启用*/;
            return useRedis;
        }

        /// <summary>
        /// 输出启动成功标志
        /// </summary>
        /// <param name="app"></param>
        public static void ShowSuccessTip(this WebApplication app)
        {
            //输出启动成功标志
            Senparc.Ncf.Core.VersionManager.ShowSuccessTip($"\t\t启动工作准备就绪\r\n\t\t用时：{SystemTime.NowDiff(StartTime).TotalSeconds} s");
        }

    }
}
