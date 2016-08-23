using System.ComponentModel;
using Infrastructure.Events;
using Prism.Events;

namespace Shell.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell
    {
        private readonly IEventAggregator _eventAggregator;

        public Shell(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            _eventAggregator.GetEvent<ApplicationCloseRequestEvent>().Publish(e);
        }
    }
}
