﻿using Prism.Modularity;
using Prism.Regions;
using System;

namespace CodeModules
{
    public class CodeModulesModule : IModule
    {
        private IRegionManager _regionManager;

        public CodeModulesModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}