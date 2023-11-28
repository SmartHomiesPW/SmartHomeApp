using FreshMvvm;
using SmartHome.Models;
using SmartHome.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartHome.Fakes
{
    public class FakeLightSwitchService : ILightSwitchService
    {
        public async Task<List<LightSwitch>> GetLightSwitches()
        {
            ICommand lightSwitchCommand = new FreshAwaitCommand(async (param, task) =>
            {
                bool result = false;
                if (param is LightSwitch lightSwitch)
                {
                    result = (lightSwitch.Status == DeviceStatus.On) ? await LightTurnOff(lightSwitch) : await LightTurnOn(lightSwitch);
                }
                task.SetResult(result);
            });

            var lightSwitchList = new List<LightSwitch>()
            {
                new LightSwitch()
                {
                    Id= "3",
                    BoardId = "2",
                    Name = "Kitchen Main Light",
                    Status = DeviceStatus.On,
                    Command = lightSwitchCommand,
                },
                new LightSwitch()
                {
                    Id= "4",
                    BoardId = "1",
                    Name = "Living Room Main Light",
                    Status = DeviceStatus.Off,
                    Command = lightSwitchCommand,
                }
            };

            return await Task.FromResult(lightSwitchList);
        }

        public async Task<bool> LightTurnOff(LightSwitch lightSwitch)
        {
            lightSwitch.Status = DeviceStatus.Off;

            return await Task.FromResult(true);
        }

        public async Task<bool> LightTurnOn(LightSwitch lightSwitch)
        {
            lightSwitch.Status |= DeviceStatus.On;

            return await Task.FromResult(true);
        }
    }
}
