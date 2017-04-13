using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    enum OS_TYPE
    {
        IOS = 0,
        ANDROID = 1
    }

    class NotificationServiceFactory
    {
        public static INotificationService Make(int osType, string certificatesFolderPath, string certificatesPassword)
        {
            OS_TYPE os = (OS_TYPE)osType;
            INotificationService service;
            switch(os)
            {
                case OS_TYPE.IOS:
                    if (string.IsNullOrEmpty(certificatesFolderPath)) return null;
                    service = new IOSNotificationService(certificatesFolderPath, certificatesPassword);
                    break;
                default:
                    service = null;
                    break;
            }
            return service;
        }
    }
}
