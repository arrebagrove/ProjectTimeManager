using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using CodeModules.Modules;
using Prism.Modularity;

namespace Shell
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Views.Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Views.Shell)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleType = typeof(ProjectBodyModule);
            ModuleCatalog.AddModule(
            new ModuleInfo()
            {
                ModuleName = moduleType.Name,
                ModuleType = moduleType.AssemblyQualifiedName,
            });
        }
    }
}
