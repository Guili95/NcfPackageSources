using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.Extensions;
using Senparc.Ncf.Core.Cache;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.Log;
using Senparc.Ncf.Service;
using Senparc.NeuChar.App.AppStore;
using Senparc.Xncf.SystemManager.ACL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Xncf.SystemManager.Domain.Service
{
    public class SystemConfigService : ClientServiceBase<SystemConfig>/*, ISystemConfigService*/
    {
        public SystemConfigService(SystemConfigRepository systemConfigRepo, IServiceProvider serviceProvider)
            : base(systemConfigRepo, serviceProvider)
        {

        }

        /// <summary>
        /// ���� NeuChar �˺�
        /// </summary>
        /// <param name="neuCharAppKey"></param>
        /// <param name="neuCharAppSecret"></param>
        /// <returns></returns>
        public async Task<string> UpdateNeuCharAccount(string neuCharAppKey = null, string neuCharAppSecret = null)
        {
            var developerId = 0;
            string appKey = null;
            string appSecret = null;

            var systemConfig = await base.GetObjectAsync(z => true);

            if (!neuCharAppKey.IsNullOrEmpty() && !neuCharAppSecret.IsNullOrEmpty())
            {
                //У�鲢��ȡ NeuCharDeveloperId
                var passportUrl = $"{Senparc.NeuChar.App.AppStore.Config.DefaultDomainName}/api/GetPassport?appKey={neuCharAppKey}&secret={neuCharAppSecret}";
                Console.WriteLine("passport:" + (passportUrl));

                var messageResultstr = await Senparc.CO2NET.HttpUtility.RequestUtility.HttpPostAsync(_serviceProvider, passportUrl, formData: new Dictionary<string, string>(), encoding: Encoding.UTF8);
                Console.WriteLine("messageResult:" + (messageResultstr != null));
                Console.WriteLine("messageResult:" + (messageResultstr));

                var messageResult = Senparc.CO2NET.Helpers.SerializerHelper.GetObject<PassportResult>(messageResultstr);

                if (messageResult.Result == AppResultKind.�ɹ�)
                {
                    developerId = messageResult.Data.DeveloperId;
                    appKey = messageResult.Data.AppKey;
                    appSecret = messageResult.Data.Secret;
                    systemConfig.UpdateNeuCharAccount(developerId, appKey, appSecret);
                    await base.SaveObjectAsync(systemConfig);
                    return "����ɹ�����Ϣ�ѱ��棡";
                }
                else
                {
                    return "AppKey �� AppSecret ����ȷ��������Ϣδ����¼�����������ã�";
                }
            }
            else
            {
                return "���ṩ��Ч�� AppKey �� AppSecret��";
            }
        }

        public SystemConfig Init(string systemName = null)
        {
            var systemConfig = GetObject(z => true);
            if (systemConfig != null)
            {
                return null;
            }

            var developerId = 0;
            string appKey = null;
            string appSecret = null;
            systemName ??= "NCF - Template Project";

            systemConfig = new SystemConfig(systemName, null, null, null, false, developerId, appKey, appSecret);
            SaveObject(systemConfig);

            return systemConfig;
        }

        public override void SaveObject(SystemConfig obj)
        {
            base.SaveObject(obj);
            LogUtility.WebLogger.InfoFormat("SystemConfig ���༭��{0}", obj.ToJson());

            //�������
            var fullSystemConfigCache = _serviceProvider.GetService<FullSystemConfigCache>();
            //ʾ��ͬ��������
            using (fullSystemConfigCache.Cache.BeginCacheLock(FullSystemConfigCache.CACHE_KEY, ""))
            {
                fullSystemConfigCache.RemoveCache();
            }
        }

        public override async Task SaveObjectAsync(SystemConfig obj)
        {
            await base.SaveObjectAsync(obj);
            LogUtility.WebLogger.InfoFormat("SystemConfig ���༭��{0}", obj.ToJson());

            //�������
            var fullSystemConfigCache = _serviceProvider.GetService<FullSystemConfigCache>();
            //ʾ��ͬ��������
            using (await fullSystemConfigCache.Cache.BeginCacheLockAsync(FullSystemConfigCache.CACHE_KEY, ""))
            {
                fullSystemConfigCache.RemoveCache();
            }
        }
    }
}

