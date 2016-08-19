using System;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CodeModules.ViewModels
{
    public class AddProjectViewModel : BindableBase, IInteractionRequestAware
    {
        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}