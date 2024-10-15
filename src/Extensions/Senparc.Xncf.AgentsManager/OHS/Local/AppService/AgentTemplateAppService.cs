﻿using Senparc.CO2NET;
using Senparc.Ncf.Core.AppServices;
using Senparc.Ncf.Core.Models;
using Senparc.Xncf.AgentsManager.Domain.Services;
using Senparc.Xncf.AgentsManager.Models.DatabaseModel;
using Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models.Dto;
using Senparc.Xncf.AgentsManager.OHS.Local.PL;
using Senparc.Xncf.PromptRange.Domain.Services;
using Senparc.Xncf.PromptRange.OHS.Local.PL.Response;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Senparc.Xncf.AgentsManager.OHS.Local.AppService
{
    public class AgentTemplateAppService : AppServiceBase
    {
        private readonly AgentsTemplateService _agentsTemplateService;
        private readonly PromptItemService _promptItemService;

        public AgentTemplateAppService(IServiceProvider serviceProvider, AgentsTemplateService agentsTemplateService, PromptItemService promptItemService) : base(serviceProvider)
        {
            this._agentsTemplateService = agentsTemplateService;
            this._promptItemService = promptItemService;
        }

        //[ApiBind]
        [FunctionRender("Agent 模板管理", "Agent 模板管理", typeof(Register))]
        public async Task<StringAppResponse> AgentTemplateManage(AgentTemplate_ManageRequest request)
        {
            return await this.GetStringResponseAsync(async (response, logger) =>
            {
                SenparcAI_GetByVersionResponse promptResult;
                var promptCode = request.GetySystemMessagePromptCode();

                try
                {
                    //检查 PromptCode 是否存在
                    promptResult = await _promptItemService.GetWithVersionAsync(promptCode, isAvg: true);
                }
                catch (Exception ex)
                {
                    // Prompt Code不存在的时候，会抛出异常
                    return ex.Message;
                }

                var promptTemplate = promptResult.PromptItem.Content;// Prompt

                var agentTemplateDto = new AgentTemplateDto(request.Name, promptCode, true,
                    request.Description, promptCode,
                    Enum.Parse<HookRobotType>(request.HookRobotType.SelectedValues.FirstOrDefault()), request.HookRobotParameter);

                await this._agentsTemplateService.UpdateAgentTemplateAsync(request.Id, agentTemplateDto);

                logger.Append("Agent 模板更新成功！");
                logger.Append("当前代理使用的 Prompt 模板：" + promptTemplate);

                return logger.ToString();
            });
        }


        /// <summary>
        /// 获取 AgentTemplate 的列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [ApiBind]
        public async Task<AppResponseBase<AgentTemplate_GetListResponse>> GetList(int pageIndex = 0, int pageSize = 0)
        {
            return await this.GetResponseAsync<AgentTemplate_GetListResponse>(async (response, logger) =>
            {
                var list = await this._agentsTemplateService.GetObjectListAsync(pageIndex, pageSize, z => true, z => z.Id, Ncf.Core.Enums.OrderingType.Descending);

                var listDto = new PagedList<AgentTemplateDto>(list.Select(z => _agentsTemplateService.Mapping<AgentTemplateDto>(z)).ToList(), list.PageIndex, list.PageCount, list.TotalCount, list.SkipCount);
                
                var result = new AgentTemplate_GetListResponse()
                {
                    List = listDto
                };
                return result;
            });
        }

        /// <summary>
        /// 获取 AgentTemplate 的详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiBind]
        public async Task<AppResponseBase<AgentTemplate_GetItemResponse>> GetItem(int id)
        {
            return await this.GetResponseAsync<AgentTemplate_GetItemResponse>(async (response, logger) =>
            {
                var agentTemplate = await this._agentsTemplateService.GetObjectAsync( z => z.Id==id, z => z.Id, Ncf.Core.Enums.OrderingType.Descending);

                var dto = this._agentsTemplateService.Mapping<AgentTemplateDto>(agentTemplate);
                var result = new  AgentTemplate_GetItemResponse()
                {
                    AgentTemplate = dto
                };

                return result;
            });
        }
    }
}
