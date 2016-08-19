using CodeModules.Views;
using Prism.Modularity;
using Prism.Regions;

namespace CodeModules.Modules
{
    public class ProjectBodyModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ProjectBodyModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof (ProjectBody));
        }
    }
}