using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using CodeModules.Models;
using CodeModules.Notifications;
using Infrastructure.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CodeModules.ViewModels
{
    public class ProjectBodyViewModel : BindableBase
    {
        private ProjectCollection _projects;
        private static DataContractSerializer ProjectSerializer { get; } = new DataContractSerializer(typeof(ProjectCollection));

        public ProjectCollection Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        public ICommand AddProjectCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public InteractionRequest<AddProjectNotification> AddProjectRequest { get; }

        public ProjectBodyViewModel(IEventAggregator eventAggregator)
        {
            Projects = new ProjectCollection();;

            if (File.Exists("projects.xml"))
            {
                using (var fs = File.Open("projects.xml", FileMode.Open))
                {
                    Projects = (ProjectCollection)ProjectSerializer.ReadObject(fs);
                }
            }

            AddProjectRequest = new InteractionRequest<AddProjectNotification>();
            AddProjectCommand = new DelegateCommand(AddProject);
            RefreshCommand = new DelegateCommand(RefreshProjects);

            eventAggregator.GetEvent<ApplicationCloseRequestEvent>().Subscribe(SaveProject);
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

        private void SaveProject(CancelEventArgs obj)
        {
            using (var fs = File.Open("projects.xml", FileMode.Create))
            {
                ProjectSerializer.WriteObject(fs, _projects);
            }

            obj.Cancel = false;
        }
    }
}