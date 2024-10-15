using Microsoft.Identity.Client;
using Senparc.Ncf.Core.Models;
using Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models;
using Senparc.Xncf.PromptRange.Models.DatabaseModel.Dto;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models.Dto
{
    /// <summary>
    /// Agent模板信息
    /// </summary>
    public class AgentTemplateDto : DtoBase<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 系统消息
        /// </summary>
        public string SystemMessage { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// PromptRange 的代号
        /// </summary>
        public string PromptCode { get; private set; }

        /// <summary>
        /// 第三方机器人平台类型
        /// </summary>
        public HookRobotType HookRobotType { get; private set; }

        /// <summary>
        /// 第三方机器人平台参数
        /// </summary>
        public string HookRobotParameter { get; set; }

        private AgentTemplateDto() { }

        public AgentTemplateDto(string name, string systemMessage, bool enable, string description, string promptCode = null, HookRobotType hookRobotType = default, string hookRobotParameter = null)
        {
            Name = name;
            SystemMessage = systemMessage;
            Enable = enable;
            Description = description;
            PromptCode = promptCode;
            HookRobotType = hookRobotType;
            HookRobotParameter = hookRobotParameter;
        }
    }

    public class AgentTemplateStatusDto
    {
        public AgentTemplateDto AgentTemplateDto { get; set; }

        public PromptItemDto PromptItemDto { get; set; }
        public PromptRangeDto PromptRangeDto { get; set; }
    }
}