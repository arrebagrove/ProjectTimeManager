using System.Collections.ObjectModel;
using System.Windows.Input;
using CodeModules.Models;
using CodeModules.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CodeModules.ViewModels
{
    public class ProjectBodyViewModel : BindableBase
    {
        private ObservableCollection<ProjectModel> _projects;

        public ObservableCollection<ProjectModel> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        public ICommand AddProjectCommand { get; private set; }

        public InteractionRequest<AddProjectNotification> AddProjectRequest { get; private set; }

        public ProjectBodyViewModel()
        {
            Projects = new ObservableCollection<ProjectModel>();

            AddProjectRequest = new InteractionRequest<AddProjectNotification>();
            AddProjectCommand = new DelegateCommand(AddProject);
        }

        private void AddProject()
        {
            var notification = new AddProjectNotification {Title = "Add Project"};

            AddProjectRequest.Raise(notification, returned =>
            {
                if (returned.Confirmed)
                    Projects.Add(new ProjectModel(returned.ProjectName, returned.Estimation));
            });
        }
    }
}