using System.Collections.Generic;

namespace Notifications.Push
{
    public interface INotification
    {
        string Title { get; }
        string Message { get; }
        string Condition { get; }
        string Builder { get; }
        string Action { get; }
        string Sound { get; }
        INotificationObject Object { get; }
        IEnumerable<IReceiver> Receivers { get; }

    }
}
