using MvvmHelpers;
using System;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class AlarmSensor : ObservableObject, IBoardDevice
    {
        private string _name;
        private DeviceStatus _status;
        private bool _movementDetected = false;

        public string Id { get; set; }
        public string BoardId { get; set; }
        public BoardDeviceType DeviceType { get => BoardDeviceType.AlarmSensor; }

        public DeviceStatus Status { get => _status; set => SetProperty(ref _status, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public bool MovementDetected { get => _movementDetected; set => SetProperty(ref _movementDetected, value); }

        public Func<object, Task<bool>> Command { get; set; }
    }
}
