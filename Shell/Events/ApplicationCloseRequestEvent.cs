using System.ComponentModel;
using Prism.Events;

namespace Shell.Events
{
    public class ApplicationCloseRequestEvent : PubSubEvent<CancelEventArgs>
    {
    }
}