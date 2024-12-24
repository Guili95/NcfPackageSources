﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.Trace;
using Senparc.Ncf.Core.Enums;
using Senparc.Ncf.Core.Exceptions;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.Repository;
using Senparc.Ncf.Service;
using Senparc.Xncf.PromptRange.Domain.Models.DatabaseModel.Dto;
using Senparc.Xncf.PromptRange.Models.DatabaseModel.Dto;
using Senparc.Xncf.PromptRange.OHS.Local.PL.Request;

namespace Senparc.Xncf.PromptRange.Domain.Services;

public class PromptRangeService : ServiceBase<Models.DatabaseModel.PromptRange>
{
    public PromptRangeService(IRepositoryBase<Models.DatabaseModel.PromptRange> repo, IServiceProvider serviceProvider) : base(repo, serviceProvider)
    {
    }

    // public async Task<PromptRangeDto> UpdateExpectedResultsAsync(int promptRangeId, string expectedResults)
    // {
    //     var promptRange = await this.GetObjectAsync(p => p.Id == promptRangeId) ??
    //                       throw new Exception($"未找到{promptRangeId}对应的靶场");
    //
    //     promptRange.UpdateExpectedResultsJson(expectedResults);
    //
    //     await this.SaveObjectAsync(promptRange);
    //
    //     return this.TransEntityToDto(promptRange);
    // }


    public async Task<PromptRangeDto> GetAsync(int Id)
    {
        var promptRange = await this.GetObjectAsync(p => p.Id == Id) ??
                          throw new Exception($"未找到{Id}对应的靶场");

        return this.TransEntityToDto(promptRange);
    }

    public async Task<List<PromptRangeDto>> GetListAsync()
    {
        var promptRange = await this.GetFullListAsync(p => true);

        return promptRange.Select(TransEntityToDto).ToList();
    }

    public async Task<PromptRangeDto> AddAsync(string alias)
    {
        try
        {
            var today = SystemTime.Now;
            var todayStr = today.ToString("yyyy.MM.dd");

            List<Models.DatabaseModel.PromptRange> todayRangeList = await this.GetFullListAsync(
                p => p.RangeName.StartsWith($"{todayStr}."),
                p => p.Id,
                OrderingType.Descending
            );

            var promptRange = new Models.DatabaseModel.PromptRange($"{todayStr}.{todayRangeList.Count + 1}", alias);

            //promptRange.ChangeAlias(alias);

            await this.SaveObjectAsync(promptRange);

            return this.TransEntityToDto(promptRange);
        }
        catch (Exception ex)
        {
            SenparcTrace.BaseExceptionLog(ex);
            await Console.Out.WriteLineAsync(ex.ToString());
            throw;
        }

    }

    public async Task<PromptRangeDto> ChangeAliasAsync(int rangeId, string alias)
    {
        var promptRange = await this.GetObjectAsync(r => r.Id == rangeId)
                          ?? throw new NcfExceptionBase($"没有找到{rangeId}对应的靶场");

        promptRange.ChangeAlias(alias);

        await this.SaveObjectAsync(promptRange);

        return this.TransEntityToDto(promptRange);
    }

    private PromptRangeDto TransEntityToDto(Models.DatabaseModel.PromptRange promptRange)
    {
        return this.Mapper.Map<PromptRangeDto>(promptRange);
    }

    public async Task<bool> DeleteAsync(int rangeId)
    {
        await base.DeleteObjectAsync(p => p.Id == rangeId);

        // todo 关联删除
        var promptItemService = _serviceProvider.GetService<PromptItemService>();

        await promptItemService.DeleteAllAsync(p => p.RangeId == rangeId);

        return true;
    }
}