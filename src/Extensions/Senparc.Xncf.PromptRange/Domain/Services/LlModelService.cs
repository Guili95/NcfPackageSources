﻿using Senparc.Ncf.Core.Exceptions;
using Senparc.Ncf.Repository;
using Senparc.Ncf.Service;
using Senparc.Xncf.PromptRange.Models;
using Senparc.Xncf.PromptRange.OHS.Local.PL.Request;
using System;
using System.Threading.Tasks;
using Senparc.AI;
using Senparc.Xncf.PromptRange.Domain.Models.DatabaseModel.Dto;
using Senparc.Xncf.PromptRange.Models.DatabaseModel.Dto;
using Senparc.Xncf.PromptRange.Domain.Models.DatabaseModel;
using Senparc.Xncf.AIKernel.Domain.Models;

namespace Senparc.Xncf.PromptRange.Domain.Services
{
    public class LlModelService : ServiceBase<LlModel>
    {
        public LlModelService(IRepositoryBase<LlModel> repo, IServiceProvider serviceProvider) : base(repo, serviceProvider)
        {
        }

        public async Task<LlModelDto> GetLlmModelById(int llmId)
        {
            var model = await base.GetObjectAsync(n => n.Id == llmId) ??
                        throw new NcfExceptionBase($"找不到{llmId}对应的模型");

            return this.Mapper.Map<LlModelDto>(model);
        }

        public async Task<LlModelDto> AddAsync(LlModel_AddRequest request)
        {
            #region validate

            // 如果是Azure OpenAI
            // if (request.ModelType == AI.AiPlatform.AzureOpenAI.ToString() || request.ModelType == AI.AiPlatform.NeuCharAI.ToString())

            if (request.ModelType is AiPlatform.AzureOpenAI)
            {
                // 强制要求ApiVersion和Endpoint不为空
                if (string.IsNullOrWhiteSpace(request.ApiVersion) || string.IsNullOrWhiteSpace(request.Endpoint))
                {
                    throw new NcfExceptionBase("使用AzureOpenAI时，ApiVersion和Endpoint不能为空");
                }

                // ApiVersion不为空且不在ApiVersionList中
                if (!string.IsNullOrWhiteSpace(request.ApiVersion) && !Constants.ApiVersionList.Contains(request.ApiVersion))
                {
                    throw new NcfExceptionBase("ApiVersion不存在");
                }
            }

            #endregion

            LlModel model = new LlModel(request);

            model.Switch(true);

            await this.SaveObjectAsync(model);

            return this.Mapper.Map<LlModelDto>(model);
        }

        public async Task<LlModelDto> UpdateAsync(LlmModel_ModifyRequest request)
        {
            var model = await this.GetObjectAsync(m => m.Id == request.Id) ??
                        throw new NcfExceptionBase("未找到该模型");

            model.Update(request.Alias, request.Show, request.DeploymentName);

            await this.SaveObjectAsync(model);

            return this.Mapper.Map<LlModelDto>(model);
        }

        public ConfigModel ConvertToConfigModel(ConfigModelType configModelType)
        {
            ConfigModel configModel;

            switch (configModelType)
            {
                case AIKernel.Domain.Models.ConfigModelType.TextCompletion:
                    configModel = ConfigModel.TextCompletion;
                    break;
                case AIKernel.Domain.Models.ConfigModelType.Chat:
                    configModel = ConfigModel.Chat;
                    break;
                case AIKernel.Domain.Models.ConfigModelType.TextToImage:
                    configModel = ConfigModel.TextToImage;
                    //TODO: Image 需要不一样的触发机制
                    break;
                case AIKernel.Domain.Models.ConfigModelType.TextEmbedding:
                case AIKernel.Domain.Models.ConfigModelType.ImageToText:
                case AIKernel.Domain.Models.ConfigModelType.TextToSpeech:
                case AIKernel.Domain.Models.ConfigModelType.SpeechToText:
                case AIKernel.Domain.Models.ConfigModelType.SpeechRecognition:
                default:
                    configModel = ConfigModel.TextCompletion;
                    break;
            }

            return configModel;
        }
    }
}