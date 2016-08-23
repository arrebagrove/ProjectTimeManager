using System;
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
        private DateTime _lastCalculationTime;
        private readonly TimeSpan _workingHourStart = new TimeSpan(10,0,0);
        private readonly TimeSpan _workingHourEnd = new TimeSpan(17, 0, 0);

        public event EventHandler ProjectCompleteEvent;

        public ProjectModel(string name, float estimation)
        {
            Name = name;
            Estimation = estimation;
            TimeRemaining = estimation;
            Status = ProjectStatus.Ready;

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
            set
            {
                SetProperty(ref _status, value);
                if (value == ProjectStatus.InProgress)
                {
                    _lastCalculationTime = DateTime.Now;
                }
                else if (value == ProjectStatus.Complete)
                {
                    OnProjectCompleteEvent();
                }
            }
        }

        public void Refresh()
        {
            if (Status == ProjectStatus.InProgress)
            {
                var timeNow = DateTime.Now;
                if (timeNow.TimeOfDay >= _workingHourStart && timeNow.TimeOfDay <= _workingHourEnd)
                {
                    var timeToDeduct = timeNow - _lastCalculationTime;
                    TimeRemaining -= (float)timeToDeduct.TotalHours;
                    _lastCalculationTime = timeNow;
                }
            }
        }

        public enum ProjectStatus
        {
            Ready,
            InProgress,
            Paused,
            Complete
        }

        protected virtual void OnProjectCompleteEvent()
        {
            var handle = ProjectCompleteEvent;
            handle?.Invoke(this, EventArgs.Empty);
        }
    }
}