using System.Windows.Input;

namespace SmartHome.Models
{
    public interface IBoardDevice
    {
        string Id { get; set; }
        string BoardId { get; set; }
        string Name { get; set; }
        BoardDeviceType DeviceType { get; }
        DeviceStatus Status { get; set; }
        ICommand Command { get; set; }
    }
}
