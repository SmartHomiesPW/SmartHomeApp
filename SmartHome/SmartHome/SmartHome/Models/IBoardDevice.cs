using System;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public interface IBoardDevice
    {
        string Id { get; set; }
        string BoardId { get; set; }
        string Name { get; set; }
        BoardDeviceType DeviceType { get; }
        DeviceStatus Status { get; set; }
        Func<object, Task<bool>> Command { get; set; }
    }
}
