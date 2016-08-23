using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Interactivity.InteractionRequest;

namespace CodeModules.Views
{
    /// <summary>
    /// Interaction logic for AddProject.xaml
    /// </summary>
    public partial class AddProjectView : UserControl
    {
        public AddProjectView()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex("[+-]([0-9]*[.])?[0-9]+");
            return !regex.IsMatch(text);
        }
    }
}
