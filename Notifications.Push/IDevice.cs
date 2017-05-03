namespace Notifications.Push
{
    public interface IDevice
    {
        string Token { get; }
        int Type { get; }
    }
}
