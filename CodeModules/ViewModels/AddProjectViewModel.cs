using System;
using System.Windows.Input;
using CodeModules.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CodeModules.ViewModels
{
    public class AddProjectViewModel : BindableBase, IInteractionRequestAware
    {
        private AddProjectNotification _notification;

        public string Name { get; set; }
        public float Estimation { get; set; }

        public ICommand AddCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public INotification Notification
        {
            get { return _notification; }
            set
            {
                var notification = value as AddProjectNotification;
                if (notification == null) return;

                _notification = notification;
                OnPropertyChanged(() => Notification);
            }
        }
        public Action FinishInteraction { get; set; }

        public AddProjectViewModel()
        {
            _notification = new AddProjectNotification();
            AddCommand = new DelegateCommand(AddInteraction);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }

        public void AddInteraction()
        {
            if (_notification != null)
            {
                _notification.ProjectName = Name;
                _notification.Estimation = Estimation;
                _notification.Confirmed = true;
            }

            FinishInteraction();
        }

        public void CancelInteraction()
        {
            if (_notification != null)
            {
                _notification.Confirmed = false;
            }

            FinishInteraction();
        }
    }
}