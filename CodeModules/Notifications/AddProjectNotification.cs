using Prism.Interactivity.InteractionRequest;

namespace CodeModules.Notifications
{
    public class AddProjectNotification : Confirmation
    {
        public string ProjectName { get; set; }
        public float Estimation { get; set; }

        public AddProjectNotification()
        {
            ProjectName = string.Empty;
            Estimation = 0.0f;
        }
    }
}