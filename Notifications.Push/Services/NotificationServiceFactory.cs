namespace Notifications.Push
{
    enum OsType
    {
        Ios = 0,
        Android = 1
    }

    class NotificationServiceFactory
    {
        /// <summary>
        /// Create a notification
        /// </summary>
        /// <param name="osType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static INotificationService Make(int osType, params string[] args)
        {
            OsType os = (OsType)osType;
            INotificationService service;
            switch (os)
            {
                case OsType.Ios:
                    if (args.Length < 2) return null;
                    service = new IOSNotificationService(args[0], args[1]);
                    break;
                case OsType.Android:
                    if (args.Length < 4) return null;
                    service = new AndroidNotificationService(args[2], args[3]);
                    break;
                default:
                    service = null;
                    break;
            }
            return service;
        }
    }
}
