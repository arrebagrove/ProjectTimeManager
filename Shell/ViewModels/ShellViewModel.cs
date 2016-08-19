using Prism.Mvvm;

namespace Shell.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string _title = "Project Time Manager";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ShellViewModel()
        {
        }
    }
}
