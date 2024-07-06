using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.Database;
using Senparc.Ncf.Database.MultipleMigrationDbContext;
using Senparc.Ncf.XncfBase.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Senparc.Xncf.DatabaseToolkit.Models.MultipleDatabase
{
    /// <summary>
    /// 用于生成 SQLServer 数据库 Migration 信息的类，请勿修改
    /// </summary>
    [MultipleMigrationDbContext(MultipleDatabaseType.Dm, typeof(Register))]
    public class DatabaseToolkitSenparcEntities_Dm : DatabaseToolkitEntities, IMultipleMigrationDbContext
    {
        public DatabaseToolkitSenparcEntities_Dm(DbContextOptions<DatabaseToolkitSenparcEntities_Dm> dbContextOptions) : base(dbContextOptions)
        {
        }
    }

    /// <summary>
    /// 设计时 DbContext 创建（仅在开发时创建 Code-First 的数据库 Migration 使用，在生产环境不会执行）
    /// <para>1、切换至 Debug 模式</para>
    /// <para>2、运行：PM> add-migration [更新名称] -C DatabaseToolkitSenparcEntities_Dm -o Domain/Migrations/Migrations.Dm </para>
    /// </summary>
    public class SenparcDbContextFactory_Dm : SenparcDesignTimeDbContextFactoryBase<DatabaseToolkitSenparcEntities_Dm, Register>
    {
        protected override Action<IApplicationBuilder> AppAction => app =>
        {
            //指定其他数据库
            app.UseNcfDatabase("Senparc.Ncf.Database.Dm", "Senparc.Ncf.Database.Dm", "DmDatabaseConfiguration");
        };

        public SenparcDbContextFactory_Dm()
            : base(
                 /* Debug模式下项目根目录
                 /* 用于寻找 App_Data 文件夹，从而找到数据库连接字符串配置信息 */
                 Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"))
        {

        }
    }
}
