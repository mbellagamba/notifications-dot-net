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
            switch (os)
            {
                case OS_TYPE.IOS:
                    if (string.IsNullOrEmpty(certificatesFolderPath)) return null;
                    service = new IOSNotificationService(certificatesFolderPath, certificatesPassword);
                    break;
                case OS_TYPE.ANDROID:
                    // TODO: implement the AndroidNotificationService and return it.
                default:
                    service = null;
                    break;
            }
            return service;
        }
    }
}
