﻿using Senparc.Ncf.Service;
using System;

namespace Senparc.Xncf.DynamicData.Areas.DynamicData.Pages
{
    public class Index : Senparc.Ncf.AreaBase.Admin.AdminXncfModulePageModelBase
    {
        public Index(Lazy<XncfModuleService> xncfModuleService) : base(xncfModuleService)
        {

        }

        public void OnGet()
        {
        }
    }
}
