using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace CodeModules.Models
{
    public class ProjectModel : BindableBase
    {
        private string _name;
        private float _estimation;
        private float _timeRemaining;
        private ProjectStatus _status;

        public ProjectModel(string name, float estimation)
        {
            Name = name;
            Estimation = estimation;
            Status = ProjectStatus.Paused;

            StartCommand = new DelegateCommand<ProjectModel>(e => Status = ProjectStatus.InProgress);
            PauseCommand = new DelegateCommand<ProjectModel>(e => Status = ProjectStatus.Paused);
            StopCommand = new DelegateCommand<ProjectModel>(e => Status = ProjectStatus.Complete);
        }

        public ICommand StartCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public float Estimation
        {
            get { return _estimation;}
            set { SetProperty(ref _estimation, value); }
        }

        public float TimeRemaining
        {
            get { return _timeRemaining; }
            set { SetProperty(ref _timeRemaining, value); }
        }

        public ProjectStatus Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        public enum ProjectStatus
        {
            InProgress,
            Paused,
            Complete
        }
    }
}