using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    enum RefreshTime
    {
        EVERY_MONTH, EVERY_DAY
    }

    static class RefreshTimeCheckService
    {

        public static bool NotNotifiedInPeriod(this IDevice device, INotification notification)
        {
            string refreshTimeKey = notification.RefreshTime;
            DateTime lastOccurrenceDate = device.LastNotifiedDate(notification);
            return IsRefreshTimeExpired(refreshTimeKey, lastOccurrenceDate);
        }

        private static bool IsRefreshTimeExpired(string refreshTimeKey, DateTime lastOccurrenceDate)
        {
            RefreshTime refreshTime;
            System.Enum.TryParse<RefreshTime>(refreshTimeKey.ToUpper(), out refreshTime);
            DateTime now = DateTime.Now;
            bool isExpired;
            switch(refreshTime)
            {
                case RefreshTime.EVERY_MONTH:
                    isExpired = HashYearMonth(now) > HashYearMonth(lastOccurrenceDate);
                    break;
                case RefreshTime.EVERY_DAY:
                    isExpired = HashYearMonthDay(now) > HashYearMonthDay(lastOccurrenceDate);
                    break;
                default:
                    // By default it consider that the refresh time is always epxired
                    isExpired = true;
                    break;
            }
            return isExpired;
        }

        private static int HashYearMonth(DateTime date)
        {
            return date.Year * 100 + date.Month;
        }

        private static int HashYearMonthDay(DateTime date)
        {
            return date.Year * 10000 + date.Month * 100 + date.Day;
        }
    }
}
