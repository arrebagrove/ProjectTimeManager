using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace CodeModules.ViewModels
{
    public class ProjectBodyViewModel : BindableBase
    {
        public ICommand AddProjectCommand { get; private set; }

        public ProjectBodyViewModel()
        {
            AddProjectCommand = new DelegateCommand(AddProject);
        }

        private void AddProject()
        {
        }
    }
}