using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    public interface IReceiver
    {
        IEnumerable<IDevice> NotificationDevices { get; }
    }
}
