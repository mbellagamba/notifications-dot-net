using System.Collections.Generic;

namespace Notifications.Push
{
    public interface INotification
    {
        int Id { get; }
        string Title { get; }
        string Message { get; }
        string Condition { get; }
        string Builder { get; }
        string Action { get; }
        string Sound { get; }
        string RefreshTime { get; }
        INotificationObject Object { get; }
        IEnumerable<IDevice> NotifiableDevices { get; }

    }
}
