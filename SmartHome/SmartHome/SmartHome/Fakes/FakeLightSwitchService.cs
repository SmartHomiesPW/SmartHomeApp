using SmartHome.Models;
using SmartHome.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    public class FakeLightSwitchService : ILightSwitchService
    {
        public async Task<List<LightSwitch>> GetLightSwitches()
        {
            var lightSwitchList = new List<LightSwitch>()
            {
                new LightSwitch()
                {
                    Id= "3",
                    BoardId = "2",
                    Name = "Kitchen Main Light",
                    Status = DeviceStatus.On,
                },
                new LightSwitch()
                {
                    Id= "4",
                    BoardId = "1",
                    Name = "Living Room Main Light",
                    Status = DeviceStatus.On,
                }
            };

            return await Task.FromResult(lightSwitchList);
        }
    }
}
