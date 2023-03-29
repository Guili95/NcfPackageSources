using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Senparc.CO2NET;
using Senparc.CO2NET.AspNet;
using Senparc.CO2NET.RegisterServices;
using Senparc.Ncf.Core;
using Senparc.Ncf.Core.Areas;
using Senparc.Ncf.Core.AssembleScan;
using Senparc.Ncf.Core.Cache;
using Senparc.Ncf.Core.Config;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.Database;
using Senparc.Ncf.Database.SqlServer;
using Senparc.Ncf.SMS;
using Senparc.Ncf.XncfBase;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Senparc.IntegrationSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //�ṩ��վ��Ŀ¼
            if (Env.ContentRootPath != null)
            {
                SiteConfig.ApplicationPath = Env.ContentRootPath;
                SiteConfig.WebRootPath = Env.WebRootPath;
            }

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddRazorPages();

            //֧�� Session
            services.AddSession();
            //������Ľ��б�������
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            //ʹ���ڴ滺��
            services.AddMemoryCache();

            //ע�� Lazy<T>
            services.AddTransient(typeof(Lazy<>));

            services.Configure<SenparcCoreSetting>(Configuration.GetSection("SenparcCoreSetting"));
            services.Configure<SenparcSmsSetting>(Configuration.GetSection("SenparcSmsSetting"));


            services.AddSenparcGlobalServices(Configuration);

            //�Զ�����ע��ɨ��
            services.ScanAssamblesForAutoDI();
            //�Ѿ���������г����Զ�ɨ���ί�У�����ִ��ɨ�裨���룩
            AssembleScanHelper.RunScan();
            services.AddHttpContextAccessor();
            //���� Xncf ��չ���棨���룩
            services.StartWebEngine(Configuration, Env);

            services.ScanAssamblesForAutoDI();

            //ָ�����ݿ����ͣ���ѡ����Ĭ��Ϊ SQLiteMemoryDatabaseConfiguration
            services.AddDatabase<SQLServerDatabaseConfiguration>();
            //services.UseDatabase<SqliteMemoryDatabaseConfiguration>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IOptions<SenparcCoreSetting> senparcCoreSetting,
            IOptions<SenparcSetting> senparcSetting)
        {
            var registerService = app
                //ȫ��ע��
                .UseSenparcGlobal(env, senparcSetting.Value, globalRegister =>
                 {
                 });

            //XncfModules�����룩
            Senparc.Ncf.XncfBase.Register.UseXncfModules(app, registerService, senparcCoreSetting.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
