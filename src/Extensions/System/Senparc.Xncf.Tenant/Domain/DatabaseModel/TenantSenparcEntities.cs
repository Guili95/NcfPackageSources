﻿using Microsoft.EntityFrameworkCore;
using Senparc.Ncf.Database;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.XncfBase.Database;
using Senparc.Ncf.Core.Models.DataBaseModel;

namespace Senparc.Xncf.SystemManager.Domain.DatabaseModel
{
    /// <summary>
    /// 当前上下文不应该和租户无关
    /// </summary>
    public class TenantSenparcEntities : XncfDatabaseDbContext
    {
        public TenantSenparcEntities(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        #region 多租户
        public DbSet<TenantInfo> TenantInfos { get; set; }

        #endregion
    }
}
