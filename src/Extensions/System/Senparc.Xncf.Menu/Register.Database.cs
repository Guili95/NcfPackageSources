﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Xncf.Menu
{
    public partial class Register : IXncfDatabase
    {
        public const string DATABASE_PREFIX = "SYSTEM_MENU_";

        public string DatabaseUniquePrefix => DATABASE_PREFIX;

        public Type TryGetXncfDatabaseDbContextType => MultipleDatabasePool.Instance.GetXncfDbContextType(this);

        public void AddXncfDatabaseModule(IServiceCollection services)
        {

        }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
