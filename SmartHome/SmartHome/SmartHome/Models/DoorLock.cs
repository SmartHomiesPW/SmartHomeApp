using MvvmHelpers;
using SmartHome.Models.BackendModels;
using System;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class DoorLock : ObservableObject, IBoardDevice
    {
        private string _name;
        private DeviceStatus _status;

        public string Id { get; set; }
        public string BoardId { get; set; }
        public BoardDeviceType DeviceType { get => BoardDeviceType.DoorLock; }

        public DeviceStatus Status { get => _status; set => SetProperty(ref _status, value); }
        public DoorLockStatus DoorStatus { get => (Status == DeviceStatus.On) ? DoorLockStatus.Open : DoorLockStatus.Closed; }
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        public Func<object, Task<bool>> Command { get; set; }

        public static DoorLock FromDoorLockBackend(DoorLockBackend doorLock, Func<object, Task<bool>> command)
        {
            return new DoorLock()
            {
                Id = doorLock.DoorLock_Id,
                BoardId = "1",
                Status = (doorLock.IsOn == 1) ? DeviceStatus.On : DeviceStatus.Off,
                Name = doorLock.Name,
                Command = command
            };
        }

        public enum DoorLockStatus { Open, Closed }
    }
}
