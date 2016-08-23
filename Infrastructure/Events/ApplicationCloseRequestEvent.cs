using System.ComponentModel;
using Prism.Events;

namespace Infrastructure.Events
{
    public class ApplicationCloseRequestEvent : PubSubEvent<CancelEventArgs>
    {
    }
}