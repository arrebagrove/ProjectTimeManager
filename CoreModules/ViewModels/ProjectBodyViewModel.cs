using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CodeModules.Models;
using CodeModules.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CodeModules.ViewModels
{
    public class ProjectBodyViewModel : BindableBase
    {
        private ObservableCollection<ProjectModel> _projects;
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<ProjectModel> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        public ICommand AddProjectCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public InteractionRequest<AddProjectNotification> AddProjectRequest { get; }

        public ProjectBodyViewModel(IEventAggregator eventAggregator)
        {
            Projects = new ObservableCollection<ProjectModel>();
            _eventAggregator = eventAggregator;

            AddProjectRequest = new InteractionRequest<AddProjectNotification>();
            AddProjectCommand = new DelegateCommand(AddProject);
            RefreshCommand = new DelegateCommand(RefreshProjects);

            _eventAggregator.GetEvent<ApplicationCloseRequestEvent>()
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

        private void RefreshProjects()
        {
            Projects.ToList().ForEach(p => p.Refresh());
        }

        
    }
}