using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows.Input;
using CodeModules.Annotations;
using Prism.Commands;

namespace CodeModules.Models
{
    [DataContract(Namespace = "http://www.shahedaziz.com/project/coremodules/projectmodel", IsReference = false)]
    public class ProjectModel : INotifyPropertyChanged
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

        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Equals(_name)) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        public float Estimation
        {
            get { return _estimation;}
            set
            {
                if (value.Equals(_estimation)) return;
                _estimation = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        public float TimeRemaining
        {
            get { return _timeRemaining; }
            set
            {
                if (value.Equals(_timeRemaining)) return;
                _timeRemaining = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        public ProjectStatus Status
        {
            get { return _status; }
            set
            {
                if (value.Equals(_status)) return;
                _status = value;
                OnPropertyChanged();

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}