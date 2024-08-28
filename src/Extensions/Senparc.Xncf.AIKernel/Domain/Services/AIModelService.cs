using Senparc.Ncf.Repository;
using Senparc.Ncf.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Ncf.Core.Exceptions;
using Senparc.Xncf.AIKernel.Domain.Models.DatabaseModel.Dto;
using Senparc.Xncf.AIKernel.Models;
using Senparc.Xncf.AIKernel.OHS.Local.PL;
using Senparc.AI.Kernel;
using Senparc.AI;
using Microsoft.Extensions.DependencyInjection;
using Senparc.AI.Kernel.Handlers;
using Senparc.AI.Entities;
using Senparc.AI.Exceptions;
using Senparc.AI.Entities.Keys;
using Senparc.Xncf.AIKernel.Domain.Models.Extensions;
using Senparc.CO2NET.Extensions;

namespace Senparc.Xncf.AIKernel.Domain.Services
{
    public class AIModelService : ServiceBase<AIModel>
    {
        public AIModelService(IRepositoryBase<AIModel> repo, IServiceProvider serviceProvider)
            : base(repo, serviceProvider)
        {
        }

        public async Task<AIModelDto> AddAsync(AIModel_CreateOrEditRequest orEditRequest)
        {
            AIModel aiModel = new AIModel(orEditRequest);
            // var aIModel = _aIModelService.Mapper.Map<AIModel>(request);

            aiModel.SwitchShow(true);

            await this.SaveObjectAsync(aiModel);

            var aiModelDto = new AIModelDto(aiModel);

            return aiModelDto;
        }

        public async Task<AIModelDto> EditAsync(AIModel_CreateOrEditRequest request)
        {
            AIModel aiModel = await this.GetObjectAsync(z => z.Id == request.Id)
                              ?? throw new NcfExceptionBase("未查询到实体!");

            #region 如果字段为空就不更新

            if (string.IsNullOrWhiteSpace(request.ApiKey))
            {
                request.ApiKey = aiModel.ApiKey;
            }
            if (string.IsNullOrWhiteSpace(request.OrganizationId))
            {
                request.OrganizationId = aiModel.OrganizationId;
            }

            #endregion

            aiModel.Update(request);

            await this.SaveObjectAsync(aiModel);

            var aiModelDto = new AIModelDto(aiModel);

            return aiModelDto;
        }

        /// <summary>
        /// 构造 SenparcAiSetting
        /// </summary>
        /// <param name="aiModel"></param>
        /// <returns></returns>
        /// <exception cref="NcfExceptionBase"></exception>
        public SenparcAiSetting BuildSenparcAiSetting(AIModelDto aiModel)
        {
            var aiSettings = new SenparcAiSetting
            {
                AiPlatform = aiModel.AiPlatform
            };

            Func<ModelName> GetModelName = () =>
            {
                ModelName modelName = new();
                switch (aiModel.ConfigModelType)
                {
                    case Models.ConfigModelType.TextCompletion:
                        modelName.TextCompletion = aiModel.ModelId;
                        break;
                    case Models.ConfigModelType.Chat:
                        modelName.Chat = aiModel.ModelId;
                        break;
                    case Models.ConfigModelType.TextEmbedding:
                        modelName.Embedding = aiModel.ModelId;
                        break;
                    case Models.ConfigModelType.TextToImage:
                        modelName.TextToImage = aiModel.ModelId;
                        break;
                    case Models.ConfigModelType.ImageToText:
                    case Models.ConfigModelType.TextToSpeech:
                    case Models.ConfigModelType.SpeechToText:
                    case Models.ConfigModelType.SpeechRecognition:
                    default:
                        throw new Exception($"尚未支持：{aiModel.ConfigModelType} 模型在 BuildSenparcAiSetting 中的处理");
                }
                return modelName;
            };

            var modelName = GetModelName();

            switch (aiSettings.AiPlatform)
            {
                case AiPlatform.NeuCharAI:
                    aiSettings.NeuCharAIKeys = new NeuCharAIKeys()
                    {
                        ApiKey = aiModel.ApiKey,
                        NeuCharAIApiVersion = aiModel.ApiVersion, // SK中实际上没有用ApiVersion
                        NeuCharEndpoint = aiModel.Endpoint,
                        ModelName = modelName,
                    };
                    aiSettings.AzureOpenAIKeys = new AzureOpenAIKeys()
                    {
                        ApiKey = aiModel.ApiKey,
                        AzureOpenAIApiVersion = aiModel.ApiVersion, // SK中实际上没有用ApiVersion
                        AzureEndpoint = aiModel.Endpoint,
                        ModelName = modelName,
                        DeploymentName = aiModel.DeploymentName
                    };
                    break;
                case AiPlatform.AzureOpenAI:
                    aiSettings.AzureOpenAIKeys = new AzureOpenAIKeys()
                    {
                        ApiKey = aiModel.ApiKey,
                        AzureOpenAIApiVersion = aiModel.ApiVersion, // SK中实际上没有用ApiVersion
                        AzureEndpoint = aiModel.Endpoint,
                        ModelName = modelName,
                        DeploymentName = aiModel.DeploymentName
                    };
                    break;
                case AiPlatform.HuggingFace:
                    aiSettings.HuggingFaceKeys = new HuggingFaceKeys()
                    {
                        Endpoint = aiModel.Endpoint,
                        ModelName = modelName,
                    };
                    break;
                case AiPlatform.OpenAI:
                    aiSettings.OpenAIKeys = new OpenAIKeys()
                    {
                        ApiKey = aiModel.ApiKey,
                        OrganizationId = aiModel.OrganizationId,
                        ModelName = modelName
                    };
                    break;
                case AiPlatform.FastAPI:
                    aiSettings.FastAPIKeys = new FastAPIKeys()
                    {
                        ApiKey = aiModel.ApiKey,
                        Endpoint = aiModel.Endpoint,
                    };
                    break;
                case AiPlatform.Ollama:
                    aiSettings.OllamaKeys = new OllamaKeys()
                    {
                        Endpoint = aiModel.Endpoint,
                    };
                    break;
                default:
                    throw new NcfExceptionBase($"Xncf.AIKernel 暂时不支持 {aiSettings.AiPlatform} 类型");
            }


            return aiSettings;
        }

        /// <summary>
        /// 运行模型
        /// </summary>
        /// <param name="senparcAiSetting"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public async Task<SenparcKernelAiResult<string>> RunModelsync(SenparcAiSetting senparcAiSetting, string prompt)
        {
            if (senparcAiSetting == null)
            {
                throw new SenparcAiException("SenparcAiSetting 不能为空");
            }

            var parameter = new PromptConfigParameter()
            {
                MaxTokens = 2000,
                Temperature = 0.7,
                TopP = 0.5,
            };

            var semanticAiHandler = base._serviceProvider.GetService<SemanticAiHandler>();
            var chatConfig = semanticAiHandler.ChatConfig(parameter, userId: "Jeffrey", maxHistoryStore: 20, senparcAiSetting: senparcAiSetting);
            var iWantToRun = chatConfig.iWantToRun;

            var request = iWantToRun.CreateRequest(prompt);
            var aiResult = await iWantToRun.RunAsync(request);
            return aiResult;
        }

        public async Task<string> UpdateModelsFromNeuCharAsync(NeuCharGetModelJsonResult modelResult, int developerId, string apiKey)
        {
            if (modelResult?.Result?.Data == null)
            {
                return "模型数据不存在，请检查是否已部署，或是否具备权限！";
            }

            var models = await base.GetFullListAsync(z => z.AiPlatform == AiPlatform.NeuCharAI);
            var updateCount = 0;
            var addCount = 0;
            foreach (var neucharModel in modelResult.Result.Data)
            {
                var model = await base.GetObjectAsync(z => z.DeploymentName == neucharModel.Name);
                var dto = new AIModel_CreateOrEditRequest()
                {
                    AiPlatform = AiPlatform.NeuCharAI,
                    ApiKey = apiKey,
                    Alias = neucharModel.Name,
                    DeploymentName = neucharModel.Name,
                    ModelId = neucharModel.Name,
                    ApiVersion = "2022-12-01",
                    Endpoint = $"https://www.neuchar.com/{developerId}",
                    ConfigModelType = Models.ConfigModelType.Chat,
                    Note = $"从 NeuChar AI 导入（DevId:{developerId}）",
                    Show = true
                };

                if (model == null)
                {
                    model = new AIModel(dto);
                    addCount++;
                }
                else
                {
                    if (!model.Note.IsNullOrEmpty())
                    {
                        dto.Note = model.Note;
                    }
                    dto.MaxToken = model.MaxToken;
                    dto.Alias = model.Alias;
                    model.Update(dto);

                    updateCount++;
                }

                await base.SaveObjectAsync(model);
            }
            return $"已成功添加 {addCount} 个模型，更新 {updateCount} 个模型信息。";
        }
    }
}