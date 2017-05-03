using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    /// <summary>
    /// The object related to the notification. Represents the entity that the mobile app should show
    /// or handle when it receives the notification.
    /// </summary>
    public interface INotificationObject
    {
        /// <summary>
        /// A unique integer identifier. If the concrete object identifier is a string hash it to an integer value.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The object name that could be used as notification title or put inside it.
        /// </summary>
        string Name { get; }
    }
}
