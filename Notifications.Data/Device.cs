namespace Notifications.Data
{
    public partial class Device
    {
        public int Id { get; set; }
        public string IdentifierForVendor { get; set; }
        public string Token { get; set; }
        public int Type { get; set; }
        public bool IsTablet { get; set; }

        public virtual User Owner { get; set; }
    }
}