﻿using System;
using System.Collections.Generic;
using Senparc.Xncf.PromptRange.Domain.Models.DatabaseModel;
using Senparc.Xncf.PromptRange.Models;
using Senparc.Xncf.PromptRange.Models.DatabaseModel.Dto;

namespace Senparc.Xncf.PromptRange.OHS.Local.PL.Response
{
    public class PromptItem_AddResponse : BaseResponse
    {
        /// <summary>
        /// 靶场　ID
        /// </summary>
        public int RangeId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        public string PromptContent { get; set; }


        /// <summary>
        /// 完整版本号
        /// </summary>
        public string FullVersion { get; set; }

        #region model config

        public int ModelId { get; set; }

        /// <summary>
        /// 最大 Token 数
        /// </summary>
        public int MaxToken { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// TopP
        /// </summary>
        public float TopP { get; set; }

        /// <summary>
        /// 频率惩罚
        /// </summary>
        public float FrequencyPenalty { get; set; }

        public float PresencePenalty { get; private set; }

        /// <summary>
        /// 停止序列（JSON 数组）
        /// </summary>
        public string StopSequences { get; set; }

        #endregion

        /// <summary>
        /// Note
        /// </summary>
        public string Note { get; set; }

        public new DateTime LastRunTime { get; set; } = DateTime.Now;

        public bool IsDraft { get; set; }
        public bool IsShare { get; set; }

        public List<PromptResultDto> PromptResultList { get; set; } = new();

        #region 打分相关

        /// <summary>
        /// 评估参数, 平均分
        /// </summary>
        public decimal EvalAvgScore { get; set; }

        /// <summary>
        /// 评估参数
        /// </summary>
        public decimal EvalMaxScore { get; set; }

        /// <summary>
        /// 期望结果Json
        /// </summary>
        public string ExpectedResultsJson { get; set; }

        #endregion

        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string VariableDictJson { get; set; }

        public PromptItem_AddResponse(int promptItemId, string promptContent, string fullVersion, int modelId,
            int maxToken, float temperature, float topP, float frequencyPenalty, float presencePenalty, string stopSequences, string note,
            string prefix, string suffix, string variableDictJson, decimal evalAvgScore, decimal evalMaxScore, string expectedResultsJson,
            bool isDraft, bool isShare, string nickName, int rangeId)
        {
            Id = promptItemId;
            PromptContent = promptContent;
            FullVersion = fullVersion;
            ModelId = modelId;
            MaxToken = maxToken;
            Temperature = temperature;
            TopP = topP;
            FrequencyPenalty = frequencyPenalty;
            PresencePenalty = presencePenalty;
            StopSequences = stopSequences;
            Note = note;
            Prefix = prefix;
            Suffix = suffix;
            VariableDictJson = variableDictJson;
            EvalAvgScore = evalAvgScore;
            EvalMaxScore = evalMaxScore;
            ExpectedResultsJson = expectedResultsJson;
            IsDraft = isDraft;
            IsShare = isShare;
            NickName = nickName;
            RangeId = rangeId;
        }

        public PromptItem_AddResponse(PromptItem item) : this(item.Id, item.Content, item.FullVersion, item.ModelId,
            item.MaxToken, item.Temperature, item.TopP, item.FrequencyPenalty, item.PresencePenalty, item.StopSequences, item.Note,
            item.Prefix, item.Suffix, item.VariableDictJson, item.EvalAvgScore, item.EvalMaxScore, item.ExpectedResultsJson,
            item.IsDraft, item.IsShare, item.NickName, item.RangeId)
        {
        }

        public PromptItem_AddResponse(PromptItemDto dto) : this(dto.Id, dto.Content, dto.FullVersion, dto.ModelId,
            dto.MaxToken, dto.Temperature, dto.TopP, dto.FrequencyPenalty, dto.PresencePenalty, dto.StopSequences, dto.Note,
            dto.Prefix, dto.Suffix, dto.VariableDictJson, dto.EvalAvgScore, dto.EvalMaxScore, dto.ExpectedResultsJson,
            dto.IsDraft, dto.IsShare, dto.NickName, dto.RangeId)
        {
        }
    }
}