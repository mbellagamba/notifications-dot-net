using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    enum BUILDER_TYPE
    {
        PRODUCT, DEFAULT
    }

    class NotificationBuildersFactory
    {
        public static INotificationBuilder Make(string type)
        {
            BUILDER_TYPE builderType;
            Enum.TryParse(type, out builderType);
            INotificationBuilder builder;
            switch(builderType)
            {
                case BUILDER_TYPE.PRODUCT:
                    builder = new ProductNotificationBuilder();
                    break;
                default:
                    builder = new DefaultNotificationBuilder();
                    break;
            }
            return builder;
        }
    }
}
