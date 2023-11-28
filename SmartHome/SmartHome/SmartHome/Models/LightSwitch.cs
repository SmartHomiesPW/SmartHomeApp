using System.Windows.Input;

namespace SmartHome.Models
{
    public class LightSwitch : IBoardDevice
    {
        public string Id { get; set; }
        public string BoardId { get; set; }
        public BoardDeviceType DeviceType { get => BoardDeviceType.LightSwitch; }
        public DeviceStatus Status { get; set; }

        public string Name { get; set; }

        public ICommand Command { get; set; }
    }
}
