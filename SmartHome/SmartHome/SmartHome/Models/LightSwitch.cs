using MvvmHelpers;
using SmartHome.Models.BackendModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartHome.Models
{
    public class LightSwitch : ObservableObject, IBoardDevice
    {
        private string _name;
        private DeviceStatus _status;

        public string Id { get; set; }
        public string BoardId { get; set; }
        public BoardDeviceType DeviceType { get => BoardDeviceType.LightSwitch; }

        public DeviceStatus Status { get => _status; set => SetProperty(ref _status, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        public Func<object, Task<bool>> Command { get; set; }

        public static LightSwitch FromLightSwitchBackend(LightSwitchBackend lightSwitch, Func<object, Task<bool>> command)
        {
            return new LightSwitch()
            {
                Id = lightSwitch.switchable_Light_Id,
                BoardId = "1",
                Status = (lightSwitch.value == 1) ? DeviceStatus.On : DeviceStatus.Off,
                Name = lightSwitch.name,
                Command = command
            };
        }
    }
}
