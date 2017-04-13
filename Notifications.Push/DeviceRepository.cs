using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Notifications.Data;

namespace Notifications.Push
{
    public class DeviceRepository
    {

        public static void Register(string udid, int type, bool isTablet, User owner)
        {
            using (var db = new NotificationsModel())
            {
                Device device = Find(db, udid);
                if (device != null)
                {
                    device.IsTablet = isTablet;
                    device.Type = type;
                    device.Owner = owner;
                }
                else
                {
                    device = new Device() { IdentifierForVendor = udid, Type = type, IsTablet = isTablet, Owner = owner };
                    db.Devices.Add(device);
                }
                db.SaveChanges();
            }
        }

        private static void Unregister(string udid)
        {
            using (var db = new NotificationsModel())
            {
                Device device = Find(db, udid);
                if (device != null)
                {
                    db.Devices.Remove(device);
                    db.SaveChanges();
                }
            }
        }

        private static Device Find(NotificationsModel db, string udid)
        {
            return db.Devices.FirstOrDefault(d => d.IdentifierForVendor.ToLower().Equals(udid.ToLower()));
        }
    }
}