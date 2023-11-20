using SmartHome.Models;
using SmartHome.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    class FakeBoardService : IBoardService
    {
        private FakeSensorService _fakeSensorService { get; set; }
        private FakeLightSwitchService _fakeLightSwitchService { get; set; }

        public FakeBoardService()
        {
            _fakeSensorService = new FakeSensorService();
            _fakeLightSwitchService = new FakeLightSwitchService();
        }

        public async Task<List<IBoardDevice>> GetDevices(string machineId)
        {
            var sensors = await _fakeSensorService.GetSensors();
            var lightSwitches = await _fakeLightSwitchService.GetLightSwitches();

            List<IBoardDevice> devices = new List<IBoardDevice>();

            devices.AddRange(sensors);
            devices.AddRange(lightSwitches);

            return await Task.FromResult(devices);
        }
    }
}
